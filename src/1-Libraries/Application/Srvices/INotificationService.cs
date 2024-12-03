// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

namespace CodeBlock.DevKit.Application.Srvices;

public interface INotificationService
{
    void Add(string notification);
    void AddRange(List<string> notifications);
    List<string> GetList();
    List<string> GetListAndReset();
    bool HasAny();
    void Reset();
}
