using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Mercado.Model;

namespace Mercado.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Editar : ContentPage
    {
        public Editar()
        {
            InitializeComponent();
        }

        private async void Salvar_Clicked(object sender, EventArgs e)
        {
            try
            {
                Produto produto_anexado = BindingContext as Produto; 

                Produto p = new Produto
                {
                    id = produto_anexado.id,
                    descricao = txt_descricao.Text,
                    quantidade = Convert.ToDouble(txt_quantidade.Text),
                    preco = Convert.ToDouble(txt_preco.Text)

                };

                await App.BD.Update(p);
                await DisplayAlert("Cadastro concluído", "Produto cadastrado com sucesso", "OK");


            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}