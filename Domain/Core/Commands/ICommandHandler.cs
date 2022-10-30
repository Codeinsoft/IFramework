using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IFramework.Domain.Core.Commands
{
    public interface ICommandHandler<TCommand> where TCommand:ICommand  
    {
        Task HandleAsync(TCommand model);
    }
}
