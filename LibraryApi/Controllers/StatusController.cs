﻿using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    public class StatusController : Controller
    {

        ISystemTime systemTime;

        public StatusController(ISystemTime systemTime)
        {
            this.systemTime = systemTime;
        }


        // Get /status -> 200 Ok
        [HttpGet("/status")]
        public ActionResult GetTheStatus()
        {
            var response = new GetStatusResponse
            {
                Message = "Everything is golden!",
                CheckedBy = "Joe",
                WhenLastChecked = systemTime.GetCurrent()
            };
            return Ok(response);
        }

        //Get /employees/123/salary     //these are called "constraints"
        [HttpGet("employees/{employeeId:int:min(1)}/salary")]
        public ActionResult GetEmployeeSalary(int employeeId)
        {
            return Ok($"The Employee {employeeId} has a salary of $72,000.00");
        }

        //Get /employees?dept=DEV
        [HttpGet("employees")]
        public ActionResult GetEmployees([FromQuery]string dept = "All")
        {
            return Ok($"Returning Employees for department {dept}");
        }

        [HttpGet("whoami")]
        public ActionResult WhoAmi(
            [FromHeader(Name = "User-Agent")]
            string userAgent)
        {
            return Ok($"I see you are running {userAgent}");
        }

        [HttpPost("employees")]
        public ActionResult HireEmployee([FromBody] EmployeeCreateRequest employeeToHire)
        {
            return Ok($"Hiring {employeeToHire.LastName} " +
                $"as a {employeeToHire.Department}");
        }

    }


    public class EmployeeCreateRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
    }


    public class GetStatusResponse
    {
        public string Message { get; set; }
        public string CheckedBy { get; set; }
        public DateTime WhenLastChecked { get; set; }
    }
}
