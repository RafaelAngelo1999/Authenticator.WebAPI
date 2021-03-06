using Authenticador.AppService.Interfaces;
using Authenticador.Infra.Data.Entities.Usuario;
using Authenticador.Infra.Data.Repositories.Base;
using Authenticator.WebAPI.Models.Usuario;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Authenticator.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;


        public LoginController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<UsuarioEntity>>> ObterUsuarios()
        {
            return await _context.Usuario.ToListAsync();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<UsuarioEntity>> ObterUsuario(Guid id)
        {
            var usuarioEntity = await _context.Usuario.FindAsync(id);

            if (usuarioEntity == null)
            {
                return NotFound();
            }

            return usuarioEntity;
        }

        [HttpPut]
        [AllowAnonymous]
        public async Task<IActionResult> AtualizarUsuario(Guid id, UsuarioEntity UsuarioEntity)
        {
            if (id != UsuarioEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(UsuarioEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExiste(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UsuarioEntity>> CriarUsuario (UsuarioInput usuarioInput)
        {
            var usuarioEntity = _mapper.Map<UsuarioInput, UsuarioEntity>(usuarioInput);

            _context.Usuario.Add(usuarioEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("ObterUsuario", new { id = usuarioEntity.Id }, usuarioEntity);
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeletarUsuario (Guid id)
        {
            var usuarioEntity = await _context.Usuario.FindAsync(id);
            if (usuarioEntity == null)
            {
                return NotFound();
            }

            _context.Usuario.Remove(usuarioEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExiste(Guid id)
        {
            return _context.Usuario.Any(e => e.Id == id);
        }
    }
}
