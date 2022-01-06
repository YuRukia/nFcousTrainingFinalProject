using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Utilities
{
    internal class ExampleCustomer
    {
        string firstName = "fName";
        string lastName = "lName";
        string billingAddress = "Example Street";
        string billingCity = "Example City";
        string billingPostcode = "TN240US";
        string billingPhone = "44 113 496 0000";
        string billingEmail = "example@email.co.uk";

        public string getFirstName() { return firstName; }
        public string getLastName() { return lastName; }
        public string getBillingAddress() { return billingAddress; }
        public string getBillingCity() { return billingCity; }
        public string getBillingPostcode() { return billingPostcode; }
        public string getBillingPhone() { return billingPhone; }
        public string getBillingEmail() { return billingEmail; }

    }
}
