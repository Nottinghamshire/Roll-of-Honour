﻿using RollOfHonour.Core.Models;

namespace RollOfHonour.Core.Shared;

public interface IPersonRepository
{
  Task<Person?> FindById(int id);
}
