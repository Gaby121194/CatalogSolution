using System;
using MediatR;


public interface ICommand<out TResult> : IRequest<out TResult>
{
}
