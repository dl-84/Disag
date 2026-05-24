using System.Threading.Tasks;

namespace Shoootz.Services.Database;

internal interface IDatabaseService
{
    Task InitializeAsync();
}
