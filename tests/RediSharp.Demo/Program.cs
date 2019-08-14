﻿using System;
using System.Threading.Tasks;
using RediSharp.Contracts;
using StackExchange.Redis;

namespace RediSharp.Demo
{
    class Program
    {
        static RedStatusResult RedisFunction(ICursor cursor, RedisValue[] args, RedisKey[] keys)
        {
            var count = (int) args[0];
            for (var i = 1; i <= count; i++)
            {
                cursor.HSet(keys[0], i, 10 + i * 2);
            }

            RedResult[] list = cursor.HMGet(keys[0], new RedisValue[] {1, 3});

            foreach (var arg in list)
            {
                cursor.Set("key_" + arg, 15);
            }

            return RedResult.Ok;
        }
        
        static RedStatusResult RedisFunction2(ICursor cursor, RedisValue[] args, RedisKey[] keys)
        {
            var count = cursor.Get(keys[0]).AsInt() ?? 0;
            var toAdd = (int) args[0];

            for (var i = 0; i < count; i++)
            {
                var key = $"{keys[0]}_{i}";
                var currentValue = cursor.Get(key).AsLong() ?? 0;
                var res = cursor.Set(key, currentValue + toAdd);
                if (!res.IsOk)
                {
                    // Reverting
                    for (var j = i - 1; j >= 0; j--)
                    {
                        key = keys[0] + "_" + j;
                        var newVal = cursor.Get(key).AsLong();
                        if (newVal != null)
                        {
                            cursor.Set(key, newVal - toAdd);
                        }
                    }

                    return RedResult.Error();
                }
            }

            return RedResult.Ok;
        }
        
        static async Task Main(string[] args)
        {
            var connection = await ConnectionMultiplexer.ConnectAsync("localhost");

            Client client = new Client(connection.GetDatabase(0));
            var handle = client.GetLuaHandle(RedisFunction2);

            // Printing Lua
            Console.WriteLine("===========================");
            Console.WriteLine(handle.Artifact);
            Console.WriteLine("===========================");

            await handle.Init();
            var res = await handle.Execute(new RedisValue[] {5}, new RedisKey[] {"countKey"});
            
            Console.WriteLine(res);
        }
    }
}