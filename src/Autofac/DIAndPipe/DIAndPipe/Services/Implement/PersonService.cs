using DIAndPipe.Entities;
using DIAndPipe.Services.Declare;
using System.Linq;

namespace DIAndPipe.Services.Implement
{
    public class PersonService:IPersonService
    {
        private EFContext _efContext;

        public PersonService(EFContext efContext)
        {
            this._efContext = efContext;
        }
        
        public Person GetPerson()
        {
            using (_efContext)
            {
                return _efContext.Persons.Select(x => x).FirstOrDefault();
            }
            //return new Person();
        }
    }
}