using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiSample.DataAccessLayer;
using WebApiSample.Model;

namespace WebApiSample.Controllers
{
    [ApiController]
    [Route("api/[Controller]")] //we are putting controller because, in line 10 we are using PersonController , so on api it will take Person and leave Controller => api/Person , if we give RamController then the api will be api/Ram
    public class PersonController : Controller
    {
        public readonly PersonDBContext personContext;

        public PersonController(PersonDBContext personContext)
        {
            this.personContext = personContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetPerson() {
            return Ok(await personContext.Persons.ToListAsync()); // ToListAsync() will return with cancellation that is if there is no value and it is null then it throws runtime error
        }

        [HttpPost]
        public async Task<ActionResult> addPerson(InsertPersonRequest insertPersonRequest)
        {
            if(insertPersonRequest == null)
            {
                return BadRequest("Person not available");
            }
            else
            {
                Person ps = new Person();

                ps.personId = new Guid();
                ps.weight = insertPersonRequest.weight;
                ps.floor_num = insertPersonRequest.floor_num;

                await personContext.Persons.AddAsync(ps);
                await personContext.SaveChangesAsync();

                return Ok(ps);
            }
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> putPerson([FromRoute]Guid id, UpdatePersonRequest updatePersonRequest)
        {
            if(updatePersonRequest == null)
            {
                return BadRequest("person not available");
            }
            else
            {
                var result = personContext.Persons.FirstOrDefault(x => x.personId.Equals(id));
                if(result == null)
                {
                    return BadRequest("Person not found");
                }
                result.weight = updatePersonRequest.weight;
                result.floor_num = updatePersonRequest.floor_num;

                personContext.Persons.Update(result);

                await personContext.SaveChangesAsync();

                return Ok(result);

            }
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> deletePerson([FromRoute] Guid id)
        {
            if(id == null)
            {
                return BadRequest("Enter valid id ");
            }
            else
            {
                var result = personContext.Persons.FirstOrDefault(x => x.personId.Equals(id));
                if(result == null)
                {
                    return BadRequest("id not found");
                }
                personContext.Persons.Remove(result);
                await personContext.SaveChangesAsync();

                return Ok(result);

            }
        }
    }
}
