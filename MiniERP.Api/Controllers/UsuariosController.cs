
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniERP.Api.Data;
using MiniERP.Api.Domain.Entities;

namespace MiniERP.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly MiniERPContext _context;

    public UsuariosController(MiniERPContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var usuarios = await _context.Usuarios.ToListAsync();

            return Ok(usuarios);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Consultar(int id)
    {
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Id == id);

        if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

        return Ok(usuario);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] Usuario usuario)
    {
        var usuarioExistente = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Id == id);

        if (usuarioExistente == null)
        {
            return NotFound("Usuário não encontrado.");
        }

        var nome = usuario.Nome?.Trim();
        var email = usuario.Email?.Trim();

        if (string.IsNullOrWhiteSpace(nome))
        {
            return BadRequest("O nome do usuário é obrigatório.");
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            return BadRequest("O e-mail do usuário é obrigatório.");
        }

        var emailNormalizado = email.ToLower();

        var existe = await _context.Usuarios.AnyAsync(u =>
        u.Id != id &&
        u.Email.Trim().ToLower() == emailNormalizado);

        if (existe)
        {
            return Conflict("Já existe um usuário com esse e-mail.");
        }

        usuarioExistente.Nome = nome;
        usuarioExistente.Email = email;

        await _context.SaveChangesAsync();

        return Ok(usuarioExistente);
    }

    [HttpPost]
    public async Task<IActionResult> Cadastrar(
        [FromBody] Usuario usuario)
    {
        var nome = usuario.Nome.Trim();
        var email = usuario.Email.Trim();

        if (string.IsNullOrWhiteSpace(nome))
        {
            return BadRequest("O nome do usuário é obrigatório.");
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            return BadRequest("O e-mail do usuário é obrigatório.");
        }

        var emailNormalizado = email.ToLower();

        var existe = await _context.Usuarios.AnyAsync(u =>
            u.Email.Trim().ToLower() == emailNormalizado);

        if (existe)
        {
            return Conflict("Já existe um usuário com esse e-mail.");
        }

        usuario.Nome = nome;
        usuario.Email = email;
        usuario.Ativo = true;

        _context.Usuarios.Add(usuario);

        await _context.SaveChangesAsync();

        return Created("api/Usuarios", usuario);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Inativar(int id)
    {
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Id == id);

        if (usuario == null)
        {
            return NotFound("Usuário não encontrado.");
        }

        usuario.Ativo = false;

        await _context.SaveChangesAsync();

        return Ok(usuario);
    }

    [HttpPatch("{id}/ativar")]
    public async Task<IActionResult> Ativar(int id)
    {
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Id == id);

        if (usuario == null)
        {
            return NotFound("Usuário não encontrado.");
        }

        usuario.Ativo = true;

        await _context.SaveChangesAsync();

        return Ok(usuario);
    }
}
