using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace pryFinal
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            frmAddUser frmAddUser= new frmAddUser();
            frmAddUser.Show();
            this.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string clave = txtClave.Text;

            var usuarios = clsUserManager.CargarUsuarios();

           
            var usuarioExistente = usuarios.FirstOrDefault(u => u.Usuario == usuario);

            if (usuarioExistente == null)
            {
                lblMensaje.Text = "El usuario no existe.";
                return;
            }

        
            if (usuarioExistente.Bloqueado)
            {
                lblMensaje.Text = "El usuario está bloqueado.";
                return;
            }

         
            if (usuarioExistente.Clave != clave)
            {
                usuarioExistente.IntentosFallidos++;

                if (usuarioExistente.IntentosFallidos >= 3)
                {
                    usuarioExistente.Bloqueado = true;
                    lblMensaje.Text = "Usuario bloqueado por intentos fallidos.";
                }
                else
                {
                    lblMensaje.Text = "Contraseña incorrecta. Intentos fallidos: " + usuarioExistente.IntentosFallidos;
                }

                clsUserManager.GuardarUsuarios(usuarios);
                return;
            }

         
            usuarioExistente.IntentosFallidos = 0;
            clsUserManager.GuardarUsuarios(usuarios);


            frmMain frm = new frmMain();
            frm.Show();
            this.Hide();  
        }

        private void lblRegistrarse_Click(object sender, EventArgs e)
        {
            new frmAddUser().ShowDialog();
        }

        private void panelPrinc_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
