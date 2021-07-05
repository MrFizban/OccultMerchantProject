using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Server;
using warehouse.items;

namespace warehouse.Controllers
{
    public class TextResult : IHttpActionResult
    {
        string _value;
        HttpRequestMessage _request;

        public TextResult(string value, HttpRequestMessage request)
        {
            _value = value;
            _request = request;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage()
            {
                Content = new StringContent(_value),
                RequestMessage = _request
            };
            return Task.FromResult(response);
        }
    }

    
    
    [ApiController]
    [Route("[controller]")]
    [EnableCors("MyPolicy")]
    public class Test
    {
        private readonly ILogger<Test> _logger;

        public Test(ILogger<Test> logger)
        {
            _logger = logger;
        }

   
        
        [HttpPost("testB")]
        public void postB([FromBody]Potion potion)
        {
            potion.saveToDatabase();

        }

        [HttpGet("testGet/id/{id}")]
        public TextResult getA(int id)
        {
            return new TextResult("ciao",new HttpRequestMessage() );
        }
        
        [HttpGet("testGet")]
        public TextResult getC()
        {
            
            var tmp = new TextResult("{'name':'name'}",new HttpRequestMessage() );
            return tmp;
        }
        
        [HttpGet("testGet/name/{name}")]
        public IEnumerable<Potion> getB(string name)
        {
            return Potion.getAll(name: name);
        }
    }

    public interface IHttpActionResult
    {
    }
}