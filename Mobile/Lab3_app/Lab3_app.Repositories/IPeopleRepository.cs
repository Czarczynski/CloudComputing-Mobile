using System;
using System.Threading.Tasks;
using RestEase;

namespace Lab3_app.Repositories
{
    public interface IPeopleRepository
    {
        [Post("people")]
        Task AddPersonAsync([Body] Person person);
    }
}
