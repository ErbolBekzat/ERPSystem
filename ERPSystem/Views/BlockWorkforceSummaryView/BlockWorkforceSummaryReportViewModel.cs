using ERPSystem.Models;
using ERPSystem.Services.BlockWorkforceSummaryServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystem.Views.BlockWorkforceSummaryView
{
    public class BlockWorkforceSummaryReportViewModel : BindableBase
    {
        IBlockWorkforceSummaryService _blockWorkforceSummaryService;

        public BlockWorkforceSummaryReportViewModel(IBlockWorkforceSummaryService blockWorkforceSummaryService)
        {
            _blockWorkforceSummaryService = blockWorkforceSummaryService;
        }

        private ObservableCollection<BlockWorkforceSummary> _bws;

        public ObservableCollection<BlockWorkforceSummary> Bws
        {
            get { return _bws; }
            set { SetProperty(ref _bws, value); }
        }

        public void LoadBWS()
        {
            Bws = new ObservableCollection<BlockWorkforceSummary>(_blockWorkforceSummaryService.GetAllBlockWorkforceSummariesForToday());
        }

    }
}
