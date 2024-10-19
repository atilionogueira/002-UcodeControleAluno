using Microsoft.VisualBasic;
using System.Threading.Tasks;

namespace Ucode.Core.Requests.ControleAluno
{
    public class GetControleAlunoByPeriodRequest : PagedRequest
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
