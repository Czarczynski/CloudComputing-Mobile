using System.Threading.Tasks;
using RestEase;

namespace Lab4_app.Repositories
{
    public interface IPeopleRepository
    {
        [Post("people")]
        Task AddPersonAsync([Body] Person person);
    }
}