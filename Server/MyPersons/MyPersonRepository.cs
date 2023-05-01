﻿using CallCenter.Data;
using CallCenter.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;

namespace CallCenter.Server.MyPersons;

public class MyPersonRepository : IMyPersonRepository
{
    private readonly CallCenterDbContext _context;
    public MyPersonRepository(CallCenterDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(MyPerson person)
    {
        await Task.Run(() =>
        {
            _context.Add(person);
            _context.SaveChanges();
        });
    }

    public async Task<MyPerson?> GetPersonAsync(int id, CancellationToken token)
    {
        MyPerson? result = await _context.Persons.Where(x => x.Id == id).FirstOrDefaultAsync<MyPerson>(token);
        return result;
    }

    public async Task<List<MyPerson>> GetPersonsAsync(CancellationToken token)
    {
        List<MyPerson> persons = await _context.Persons.ToListAsync(token);
        return persons ?? new();
    }

    public async Task<bool> GetPersonExistsAsync(int id, CancellationToken token)
    {
        bool result = await _context.Persons.AnyAsync(x => x.Id == id, token);
        return result;
    }
}
