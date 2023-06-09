﻿namespace Backups.Models.Abstractions;

public interface IRepositoryAccessKey
{
    string FullKey { get; }
    string Name { get; }
    IReadOnlyCollection<string> KeyParts { get; }

    IRepositoryAccessKey Combine(IRepositoryAccessKey other);
    IRepositoryAccessKey Combine(string value);

    IRepositoryAccessKey ApplyExtension(string extension);
}