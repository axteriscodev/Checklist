using Radzen.Blazor;
using Shared;

namespace ClientWebApp.Components.Choices
{
    public partial class TableChoice
    {
        /// <summary>
        /// booleano che indica se la pagina sta eseguendo il caricamento iniziale
        /// </summary>
        private bool initialLoading;
        /// <summary>
        /// Booleano che è impostata durante una ricerca
        /// </summary>
        private bool isLoading = false;
        /// <summary>
        /// Intero che ci dice quanti sono gli elementi
        /// </summary>
        private int count;
        /// <summary>
        /// Intero che ci dice quanti elementi possono stare in una pagina
        /// </summary>
        private int pageSize = 8;
        /// <summary>
        /// Stringa indica la pagina e gli elementi
        /// </summary>
        private string pagingSummaryFormat = "Pagina {0} di {1} (Totale {2} campi misurazione)";
        /// <summary>
        /// Riferimento al componente tabella
        /// </summary>
        private RadzenDataGrid<ChoiceModel>? grid;




        private async Task ApriFormNuovo()
        {

        }

        private async Task ApriFormModifica()
        {

        }
    }
}
