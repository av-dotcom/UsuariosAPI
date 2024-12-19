using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Models;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private static List<Usuario> Usuarios = new List<Usuario>();

    [HttpGet]
    public ActionResult<IEnumerable<Usuario>> GetUsuarios()
    {
        return Ok(Usuarios);
    }

    [HttpGet("{id}")]
    public ActionResult<Usuario> GetUsuarioById(int id)
    {
        try
        {
            var usuario = Usuarios.Find(u => u.Id == id);
            if (usuario == null)
                return NotFound("Usuario no encontrado.");
            return Ok(usuario);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ocurrió un error al obtener el usuario: {ex.Message}");
        }
    }

    [HttpPost]
    public ActionResult AddUsuario([FromBody] Usuario usuario)
    {
        if (string.IsNullOrWhiteSpace(usuario.Nombre))
        {
            return BadRequest("El nombre es obligatorio.");
        }

        if (
            string.IsNullOrWhiteSpace(usuario.CorreoElectronico)
            || !usuario.CorreoElectronico.Contains("@")
        )
        {
            return BadRequest("El correo electrónico no es válido.");
        }

        usuario.Id = Usuarios.Count + 1;
        usuario.FechaRegistro = DateTime.Now;
        Usuarios.Add(usuario);
        return CreatedAtAction(nameof(GetUsuarioById), new { id = usuario.Id }, usuario);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateUsuario(int id, [FromBody] Usuario updatedUsuario)
    {
        var usuario = Usuarios.Find(u => u.Id == id);
        if (usuario == null)
        {
            return NotFound("Usuario no encontrado.");
        }

        if (string.IsNullOrWhiteSpace(updatedUsuario.Nombre))
        {
            return BadRequest("El nombre es obligatorio.");
        }

        if (
            string.IsNullOrWhiteSpace(updatedUsuario.CorreoElectronico)
            || !updatedUsuario.CorreoElectronico.Contains("@")
        )
        {
            return BadRequest("El correo electrónico no es válido.");
        }

        usuario.Nombre = updatedUsuario.Nombre;
        usuario.CorreoElectronico = updatedUsuario.CorreoElectronico;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteUsuario(int id)
    {
        var usuario = Usuarios.Find(u => u.Id == id);
        if (usuario == null)
            return NotFound();
        Usuarios.Remove(usuario);
        return NoContent();
    }

    [HttpGet("filtrar")]
    public ActionResult<IEnumerable<Usuario>> FiltrarUsuariosPorDominio(string dominio)
    {
        var usuariosFiltrados = Usuarios.FindAll(u => u.CorreoElectronico.EndsWith($"@{dominio}"));
        return Ok(usuariosFiltrados);
    }
}
