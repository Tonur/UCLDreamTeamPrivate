﻿using System;
using System.Collections.Generic;
using System.Linq;
using ResourceInterface;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ResourceMicrosDtabase;
using Business_Layer.Models;
using ResourceMicrosDtabase.Models;

namespace ResourceMicroService.BusinessLayer
{
    public class ResourceService
    {
        private ReasourceContext _applicationContext;

        public ResourceService(ReasourceContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        #region Create
        //Creates a resource
        public ApiResponse<Resource<AvaiableTime>> Create(Resource<AvaiableTime> resource)
        {
            bool invalidDate = false;

            try
            {
                if (resource == null)
                {
                    return new ApiResponse<Resource<AvaiableTime>>(ApiResponseCode.NoContent, null);
                }

                foreach (var timeslot in resource.TimeSlots)
                {
                    timeslot.Id = Guid.NewGuid();
                }

                //Checks that the timeslots do not overlap.
                resource.TimeSlots.ForEach(delegate (AvaiableTime outerTime)
                {
                    resource.TimeSlots.ForEach(delegate (AvaiableTime innerTime)
                    {
                        if (!(((outerTime.From <= innerTime.From) && (outerTime.To <= innerTime.From)) ||
                        ((outerTime.From >= innerTime.To) && (outerTime.To >= innerTime.To))))
                        {
                            if (outerTime.Id != innerTime.Id)
                            {
                                invalidDate = true;
                            }
                        }
                    });
                });

                if (!invalidDate)
                {
                    _applicationContext.Add(resource);
                    _applicationContext.SaveChanges();

                    return new ApiResponse<Resource<AvaiableTime>>(ApiResponseCode.OK, resource);
                }
                else
                {
                    return new ApiResponse<Resource<AvaiableTime>>(ApiResponseCode.NotModified, null);
                }
            }
            catch (Exception)
            {
                return new ApiResponse<Resource<AvaiableTime>>(ApiResponseCode.InternalServerError, null);
            }
        }
        #endregion
        #region Get
        //Get all resources
        public ApiResponse<List<Resource<AvaiableTime>>> Get()
        {
            try
            {   //Returns a list of Resources, with all of its children
                return new ApiResponse<List<Resource<AvaiableTime>>>
                    (ApiResponseCode.OK, _applicationContext.Resources
                    .Include(resource => resource.TimeSlots)
                    .ToList());
            }
            catch (Exception)
            {
                return new ApiResponse<List<Resource<AvaiableTime>>>(ApiResponseCode.InternalServerError, null);
            }
        }

        //Get a specific resource from a GUID
        public ApiResponse<Resource<AvaiableTime>> Get(Guid guid)
        {
            Resource<AvaiableTime> resourceToReturn = new Resource<AvaiableTime>();

            try
            {
                //Gets a resource with all of its children
               resourceToReturn = _applicationContext.Resources
                    .Include(resource => resource.TimeSlots)
                    .SingleOrDefault(resource => resource.Id == guid);

                if (resourceToReturn == null)
                {
                    return new ApiResponse<Resource<AvaiableTime>>(ApiResponseCode.NoContent, null);
                }
                else
                {
                    return new ApiResponse<Resource<AvaiableTime>>(ApiResponseCode.OK, resourceToReturn);
                }
            }
            catch (Exception)
            {
                return new ApiResponse<Resource<AvaiableTime>>(ApiResponseCode.InternalServerError, null);
            }
        }
        #endregion
        #region Update
        //Update a resource
        //Should ignore reservations when being updated
        public ApiResponse<Resource<AvaiableTime>> Update(Resource<AvaiableTime> resource)
        {
            Resource<AvaiableTime> resourceToUpdate = new Resource<AvaiableTime>();

            bool invalidDate = false;

            try
            {
                resourceToUpdate = _applicationContext.Resources.Include(r => r.TimeSlots).SingleOrDefault(r => r.Id == resource.Id);

                if (resourceToUpdate == null)
                {
                    return new ApiResponse<Resource<AvaiableTime>>(ApiResponseCode.NoContent, null);
                }

                //Checks that the timeslots do not overlap. Also makes sure that he id is not the same, if the times do match.
                resource.TimeSlots.ForEach(delegate (AvaiableTime newTimeSlot)
                {
                    resourceToUpdate.TimeSlots.ForEach(delegate (AvaiableTime existingTimeSlot)
                    {
                        if (!(((newTimeSlot.From <= existingTimeSlot.From) && (newTimeSlot.To <= existingTimeSlot.From)) ||
                        ((newTimeSlot.From >= existingTimeSlot.To) && (newTimeSlot.To >= existingTimeSlot.To))))
                        {
                            if (newTimeSlot.Id != existingTimeSlot.Id)
                            {
                                invalidDate = true;
                            }
                        }
                    });
                });

                if (!invalidDate)
                {
                    resourceToUpdate.Name = resource.Name;
                    resourceToUpdate.Description = resource.Description;
                    resourceToUpdate.TimeSlots = resource.TimeSlots;

                    _applicationContext.Update(resourceToUpdate);
                    _applicationContext.SaveChanges();

                    return new ApiResponse<Resource<AvaiableTime>>(ApiResponseCode.OK, resourceToUpdate);
                }
                else
                {
                    return new ApiResponse<Resource<AvaiableTime>>(ApiResponseCode.NotModified, null);
                }
            }
            catch (Exception)
            {
                return new ApiResponse<Resource<AvaiableTime>>(ApiResponseCode.InternalServerError, null);
            }
        }
        #endregion
        #region Delete
        //Delete a resource
        public ApiResponse<Resource<AvaiableTime>> Delete(Guid guid)
        {
            Resource<AvaiableTime> resourceToDelete = new Resource<AvaiableTime>();

            try
            {
                //Gets the resource to be deleted. All children are tracked as well, so that they are also deleted.
                resourceToDelete = _applicationContext.Resources
                    .Include(resource => resource.TimeSlots)
                    .SingleOrDefault(resource => resource.Id == guid);

                if (resourceToDelete == null)
                {
                    return new ApiResponse<Resource<AvaiableTime>>(ApiResponseCode.NoContent, null);
                }
                else
                {
                    //This will cascade delete.
                    _applicationContext.Resources.Remove(resourceToDelete);

                    _applicationContext.SaveChanges();


                    return new ApiResponse<Resource<AvaiableTime>>(ApiResponseCode.OK, null);
                }
            }
            catch (Exception)
            {
                return new ApiResponse<Resource<AvaiableTime>>(ApiResponseCode.InternalServerError, null);
            }
        }
        #endregion
    }
}
