﻿namespace EventMaster.Application.Common.Interfaces.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;