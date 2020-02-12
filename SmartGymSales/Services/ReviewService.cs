using SmartGymSales.enums;
using SmartGymSales.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace SmartGymSales.Services
{
    public class ReviewService
    {
        public List<String> insertReview(string user_name, string password, review review)
        {
            List<String> errors = new List<string>();

            using (var db = new SmartGymSalesEntities())
            {
                bool reviewError = false;
                UtilsService US = new UtilsService();
                UsersService userService = new UsersService();
                UserRolesService userRolesService = new UserRolesService();
                User currentUser = userService.GetUserbyUser_name(user_name);
                if (!userService.checkUserCred(user_name, password))
                {
                    errors.Add("please Login and try again");
                    return errors;
                }
                if (!userRolesService.isUserSales(user_name))
                {
                    errors.Add("You are not authorized");
                    return errors;
                }

                if (String.IsNullOrEmpty(review.comment))
                {
                    errors.Add("Comment field is empty");
                    reviewError = true;
                }
                if (review.reciption > 5 || review.reciption == 0)
                {
                    errors.Add("Reciption rate field is not valid");
                    reviewError = true;
                }
                if (review.training > 5 || review.training == 0)
                {
                    errors.Add("Training rate field is not valid");
                    reviewError = true;
                }
                if (review.general > 5 || review.general == 0)
                {
                    errors.Add("The General rate field is not valid");
                    reviewError = true;
                }
                review.creation_date = DateTime.Now;

                //handle if parent possibleCustomer
                if (review.parent_id_type == ParentModalEnum.PossibleCustomer.ToDescriptionString())
                {
                    possibleCustomer ps = db.possibleCustomers.Where(x => x.id == review.parent_id).FirstOrDefault();
                    if (ps == null)
                    {
                        errors.Add("Couldn't find the related customer to this review in the db ");
                        reviewError = true;
                    }
                    else
                    {
                        //not allowed to call because this customer is call by another user (to be updated after the user deleted )
                        if (ps.is_called_by != null && ps.is_called_by != currentUser.id)
                        {
                            errors.Add("this possible customer is associated with another user");
                            reviewError = true;
                        }
                        else
                        {
                            if (ps.is_called_by == null)
                            { ps.is_called_by = currentUser.id; }
                            ps.is_called = true;
                            ps.last_call_date = DateTime.Now;
                            ps.calles_count = ps.calles_count + 1;
                            db.possibleCustomers.AddOrUpdate(ps);
                            db.SaveChanges();
                        }
                    }

                }
                //handle if parent is customer
                else if (review.parent_id_type == ParentModalEnum.Customers.ToDescriptionString())
                {
                    SalesCustomer customer = db.SalesCustomers.Where(x => x.id == review.parent_id).FirstOrDefault();

                    if (customer == null)
                    {
                        errors.Add("Couldn't find the related customer to this review in the db ");
                        reviewError = true;
                    }
                    else
                    {
                        if (customer.is_called_by != null && customer.is_called_by != currentUser.id)
                        {
                            errors.Add("this customer is associated with another user");
                            reviewError = true;
                        }
                        else
                        {
                            if (customer.is_called_by == null)
                            { customer.is_called_by = currentUser.id; }
                            customer.is_called = true;
                            customer.last_call_date = DateTime.Now;
                            customer.calles_count = customer.calles_count + 1;
                            db.SalesCustomers.AddOrUpdate(customer);
                            db.SaveChanges();
                        }

                    }
                }
                else
                {
                    errors.Add("Couldn't find the related customer to this review in the db ");
                    reviewError = true;
                }

                if (!reviewError)
                {
                    db.reviews.Add(review);
                    db.SaveChanges();
                }
            }
            return errors;

        }

        public List<OutputReview> getAllReviews(string user_name, string password)
        {
            List<OutputReview> result = new List<OutputReview>();

            using (var db = new SmartGymSalesEntities())
            {
                UsersService userService = new UsersService();
                UserRolesService userRolesService = new UserRolesService();
                User currentUser = userService.GetUserbyUser_name(user_name);
                if (!userService.checkUserCred(user_name, password))
                {
                    return new List<OutputReview>();
                }
                if (!userRolesService.isUserSales(user_name))
                {
                    return new List<OutputReview>();
                }
                List<review> reviews = db.reviews.ToList();
                foreach (review item in reviews)
                {
                    OutputReview outputResult = new OutputReview();
                    outputResult.id = item.id;
                    outputResult.comment = item.comment;
                    outputResult.general = item.general;
                    outputResult.training = item.training;
                    outputResult.reciption = item.reciption;
                    outputResult.creation_date = item.creation_date;
                    outputResult.parent_id = item.parent_id;
                    outputResult.parent_type = item.parent_id_type;
                    if (item.parent_id_type == ParentModalEnum.PossibleCustomer.ToDescriptionString())
                    {
                        PossibleCustomersService pcService = new PossibleCustomersService();
                        possibleCustomer PC = pcService.getPossibleCustomerById(item.parent_id);
                        if (PC != null)
                        {
                            outputResult.parent_name = PC.name;
                            result.Add(outputResult);
                        }
                    }
                    else if (item.parent_id_type == ParentModalEnum.Customers.ToDescriptionString())
                    {
                        CustomerService customerService = new CustomerService();
                        SalesCustomer sc = customerService.getSalesCustomers(item.parent_id);
                        if (sc != null)
                        {
                            outputResult.parent_name = sc.name;
                            result.Add(outputResult);
                        }
                    }
                }
            }
            return result;
        }

    }
}