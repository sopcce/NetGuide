using DIAndPipe.Entities;
using DIAndPipe.Services.Declare;

namespace DIAndPipe.Services.Implement
{
    /// <summary>
    /// 另一个实现了IPersonService接口的服务
    /// </summary>
    public class OtherPersonService:IPersonService
    {
        public Person GetPerson()
        {
            return new Person()
            {
               Name = "I come from another service"
            };
        }
    }
}