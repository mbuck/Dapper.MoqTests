﻿using System.Collections.Generic;

namespace Dapper.MoqTests.Samples
{
    public class SampleRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public SampleRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IEnumerable<Car> GetCars()
        {
            using (var connection = _connectionFactory.OpenConnection())
            {
                return connection.Query<Car>(@"select * 
from [Cars] 
order by Make, Model");
            }
        }

        public Car GetCar(string registration)
        {
            using (var connection = _connectionFactory.OpenConnection())
            {
                return connection.QuerySingle<Car>(@"select * 
from [Cars] 
where Registration = @registration", new { registration });
            }
        }

        public IEnumerable<string> GetModels(string make)
        {
            using (var connection = _connectionFactory.OpenConnection())
            {
                return connection.Query<string>(@"select distinct Model 
from [Cars] 
where Make = @make
order by Model", new { make });
            }
        }

        public void DeleteCar(string registration)
        {
            using (var connection = _connectionFactory.OpenConnection())
            {
                connection.Execute(@"delete from [Cars] 
where Registration = @registration", new { registration });
            }
        }
    }
}
