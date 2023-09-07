using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPSystem.Models
{
    public class BlockWorkforceSummary : BindableBase
    {
        private int _id;
        private int _projectId;
        private string _projectName;
        private int _blockId;
        private string _blockName;
        private string _foreman;
        private string _leadWorker;

        [Column(TypeName = "timestamp without time zone")]
        private DateTime _date;

        private int? _monolithicReinforcedConcrete;
        private string? _monolithicReinforcedConcreteForeman;

        private int? _wallReinforcement;
        private string? _wallReinforcementForeman;

        private int? _aeratedBlockWall;
        private string? _aeratedBlockWallForeman;

        private int? _electricalInstallations;
        private string? _electricalInstallationsForeman;

        private int? _plumbingWorks;
        private string? _plumbingWorksForeman;

        private int? _ventilationSystemInstallation;
        private string? _ventilationSystemInstallationForeman;

        private int? _wallPlastering;
        private string? _wallPlasteringForeman;

        private int? _semiDryFloorScreed;
        private string? _semiDryFloorScreedForeman;

        private int? _waterproofing;
        private string? _waterproofingForeman;

        private int? _earthworks;
        private string? _earthworksForeman;

        private int? _cleaning;
        private string? _cleaningForeman;

        private int? _windowBlockInstallation;
        private string? _windowBlockInstallationForeman;

        private int? _facade;
        private string? _facadeForeman;

        private int? _metalLiftStructureInstallation;
        private string? _metalLiftStructureInstallationForeman;

        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        public int ProjectId
        {
            get { return _projectId; }
            set { SetProperty(ref _projectId, value); }
        }

        public string ProjectName
        {
            get { return _projectName; }
            set { SetProperty(ref _projectName, value); }
        }

        public int BlockId
        {
            get { return _blockId; }
            set { SetProperty(ref _blockId, value); }
        }

        public string BlockName
        {
            get { return _blockName; }
            set { SetProperty(ref _blockName, value); }
        }

        public string Foreman
        {
            get { return _foreman; }
            set { SetProperty(ref _foreman, value); }
        }

        public string LeadWorker
        {
            get { return _leadWorker; }
            set { SetProperty(ref _leadWorker, value); }
        }

        public DateTime Date
        {
            get { return _date; }
            set { SetProperty(ref _date, value); }
        }

        public int? MonolithicReinforcedConcrete
        {
            get { return _monolithicReinforcedConcrete; }
            set { SetProperty(ref _monolithicReinforcedConcrete, value); }
        }

        public string? MonolithicReinforcedConcreteForeman
        {
            get { return _monolithicReinforcedConcreteForeman; }
            set { SetProperty(ref _monolithicReinforcedConcreteForeman, value); }
        }

        public int? WallReinforcement
        {
            get { return _wallReinforcement; }
            set { SetProperty(ref _wallReinforcement, value); }
        }

        public string? WallReinforcementForeman
        {
            get { return _wallReinforcementForeman; }
            set { SetProperty(ref _wallReinforcementForeman, value); }
        }

        public int? AeratedBlockWall
        {
            get { return _aeratedBlockWall; }
            set { SetProperty(ref _aeratedBlockWall, value); }
        }

        public string? AeratedBlockWallForeman
        {
            get { return _aeratedBlockWallForeman; }
            set { SetProperty(ref _aeratedBlockWallForeman, value); }
        }

        public int? ElectricalInstallations
        {
            get { return _electricalInstallations; }
            set { SetProperty(ref _electricalInstallations, value); }
        }

        public string? ElectricalInstallationsForeman
        {
            get { return _electricalInstallationsForeman; }
            set { SetProperty(ref _electricalInstallationsForeman, value); }
        }

        public int? PlumbingWorks
        {
            get { return _plumbingWorks; }
            set { SetProperty(ref _plumbingWorks, value); }
        }

        public string? PlumbingWorksForeman
        {
            get { return _plumbingWorksForeman; }
            set { SetProperty(ref _plumbingWorksForeman, value); }
        }

        public int? VentilationSystemInstallation
        {
            get { return _ventilationSystemInstallation; }
            set { SetProperty(ref _ventilationSystemInstallation, value); }
        }

        public string? VentilationSystemInstallationForeman
        {
            get { return _ventilationSystemInstallationForeman; }
            set { SetProperty(ref _ventilationSystemInstallationForeman, value); }
        }

        public int? WallPlastering
        {
            get { return _wallPlastering; }
            set { SetProperty(ref _wallPlastering, value); }
        }

        public string? WallPlasteringForeman
        {
            get { return _wallPlasteringForeman; }
            set { SetProperty(ref _wallPlasteringForeman, value); }
        }

        public int? SemiDryFloorScreed
        {
            get { return _semiDryFloorScreed; }
            set { SetProperty(ref _semiDryFloorScreed, value); }
        }

        public string? SemiDryFloorScreedForeman
        {
            get { return _semiDryFloorScreedForeman; }
            set { SetProperty(ref _semiDryFloorScreedForeman, value); }
        }

        public int? Waterproofing
        {
            get { return _waterproofing; }
            set { SetProperty(ref _waterproofing, value); }
        }

        public string? WaterproofingForeman
        {
            get { return _waterproofingForeman; }
            set { SetProperty(ref _waterproofingForeman, value); }
        }

        public int? Earthworks
        {
            get { return _earthworks; }
            set { SetProperty(ref _earthworks, value); }
        }

        public string? EarthworksForeman
        {
            get { return _earthworksForeman; }
            set { SetProperty(ref _earthworksForeman, value); }
        }

        public int? Cleaning
        {
            get { return _cleaning; }
            set { SetProperty(ref _cleaning, value); }
        }

        public string? CleaningForeman
        {
            get { return _cleaningForeman; }
            set { SetProperty(ref _cleaningForeman, value); }
        }

        public int? WindowBlockInstallation
        {
            get { return _windowBlockInstallation; }
            set { SetProperty(ref _windowBlockInstallation, value); }
        }

        public string? WindowBlockInstallationForeman
        {
            get { return _windowBlockInstallationForeman; }
            set { SetProperty(ref _windowBlockInstallationForeman, value); }
        }

        public int? Facade
        {
            get { return _facade; }
            set { SetProperty(ref _facade, value); }
        }

        public string? FacadeForeman
        {
            get { return _facadeForeman; }
            set { SetProperty(ref _facadeForeman, value); }
        }

        public int? MetalLiftStructureInstallation
        {
            get { return _metalLiftStructureInstallation; }
            set { SetProperty(ref _metalLiftStructureInstallation, value); }
        }

        public string? MetalLiftStructureInstallationForeman
        {
            get { return _metalLiftStructureInstallationForeman; }
            set { SetProperty(ref _metalLiftStructureInstallationForeman, value); }
        }
    }
}
