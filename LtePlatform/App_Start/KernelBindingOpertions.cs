﻿using Abp.EntityFramework;
using Lte.Evaluations.DataService;
using Lte.Evaluations.DataService.Basic;
using Lte.Evaluations.DataService.College;
using Lte.Evaluations.DataService.Dump;
using Lte.Evaluations.DataService.Kpi;
using Lte.Evaluations.DataService.Mr;
using Lte.Evaluations.DataService.Switch;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Concrete;
using Lte.Parameters.Abstract;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Abstract.College;
using Lte.Parameters.Abstract.Kpi;
using Lte.Parameters.Abstract.Neighbor;
using Lte.Parameters.Abstract.Switch;
using Lte.Parameters.Concrete;
using Lte.Parameters.Concrete.Basic;
using Lte.Parameters.Concrete.Channel;
using Lte.Parameters.Concrete.College;
using Lte.Parameters.Concrete.Kpi;
using Lte.Parameters.Concrete.Mr;
using Lte.Parameters.Concrete.Neighbor;
using Lte.Parameters.Concrete.Switch;
using Ninject;

namespace LtePlatform
{
    public static class KernelBindingOpertions
    {
        public static void AddBindings(this IKernel ninjectKernel)
        {
            ninjectKernel.Bind<EFParametersContext>().ToSelf();

            ninjectKernel.Bind<IDbContextProvider<EFParametersContext>>()
                .To<SimpleDbContextProvider<EFParametersContext>>();

            ninjectKernel.Bind<MySqlContext>().ToSelf();

            ninjectKernel.Bind<IDbContextProvider<MySqlContext>>().To<SimpleDbContextProvider<MySqlContext>>();

            ninjectKernel.Bind<ITownRepository>().To<EFTownRepository>();

            ninjectKernel.Bind<ICdmaRegionStatRepository>().To<EFCdmaRegionStatRepository>();

            ninjectKernel.Bind<IRegionRepository>().To<EFRegionRepository>();

            ninjectKernel.Bind<ICollegeRepository>().To<EFCollegeRepository>();

            ninjectKernel.Bind<IInfrastructureRepository>().To<EFInfrastructureRepository>();

            ninjectKernel.Bind<IAlarmRepository>().To<EFAlarmRepository>();

            ninjectKernel.Bind<ICollege3GTestRepository>().To<EFCollege3GTestRepository>();

            ninjectKernel.Bind<ICollege4GTestRepository>().To<EFCollege4GTestRepository>();

            ninjectKernel.Bind<ICollegeKpiRepository>().To<EFCollegeKpiRepository>();

            ninjectKernel.Bind<IENodebRepository>().To<EFENodebRepository>();

            ninjectKernel.Bind<IBtsRepository>().To<EFBtsRepository>();

            ninjectKernel.Bind<ICellRepository>().To<EFCellRepository>();

            ninjectKernel.Bind<ICdmaCellRepository>().To<EFCdmaCellRepository>();

            ninjectKernel.Bind<IIndoorDistributioinRepository>().To<EFIndoorDistributionRepository>();

            ninjectKernel.Bind<IPreciseCoverage4GRepository>().To<EFPreciseCoverage4GRepository>();

            ninjectKernel.Bind<ITopDrop2GCellRepository>().To<EFTopDrop2GCellRepository>();

            ninjectKernel.Bind<ITopConnection3GRepository>().To<EFTopConnection3GRepository>();

            ninjectKernel.Bind<ITownPreciseCoverage4GStatRepository>().To<EFTownPreciseCoverage4GStatRepository>();

            ninjectKernel.Bind<IAreaTestDateRepository>().To<MasterAreaTestDateDateRepository>();

            ninjectKernel.Bind<ICsvFileInfoRepository>().To<MasterCsvFileInfoRepository>();

            ninjectKernel.Bind<IRasterInfoRepository>().To<MasterRasterInfoRepository>();

            ninjectKernel.Bind<ILteNeighborCellRepository>().To<EFLteNeighborCellRepository>();

            ninjectKernel.Bind<INearestPciCellRepository>().To<EFNearestPciCellRepository>();

            ninjectKernel.Bind<IWorkItemRepository>().To<EFWorkItemRepository>();

            ninjectKernel.Bind<IInterferenceMatrixRepository>().To<EFInterferenceMatrixRepository>();

            ninjectKernel.Bind<IInterferenceMongoRepository>().To<InterferenceMongoRepository>();

            ninjectKernel.Bind<ICellHuaweiMongoRepository>().To<CellHuaweiMongoRepository>();

            ninjectKernel.Bind<ICellMeasGroupZteRepository>().To<CellMeasGroupZteRepository>();

            ninjectKernel.Bind<IUeEUtranMeasurementRepository>().To<UeEUtranMeasurementRepository>();

            ninjectKernel.Bind<IIntraFreqHoGroupRepository>().To<IntraFreqHoGroupRepository>();

            ninjectKernel.Bind<IIntraRatHoCommRepository>().To<IntraRatHoCommRepository>();

            ninjectKernel.Bind<IInterRatHoCommRepository>().To<InterRatHoCommRepository>();

            ninjectKernel.Bind<IEutranInterNFreqRepository>().To<EutranInterNFreqRepository>();

            ninjectKernel.Bind<IInterFreqHoGroupRepository>().To<InterFreqHoGroupRepository>();

            ninjectKernel.Bind<IEUtranRelationZteRepository>().To<EUtranRelationZteRepository>();

            ninjectKernel.Bind<IEutranIntraFreqNCellRepository>().To<EutranIntraFreqNCellRepository>();

            ninjectKernel.Bind<IExternalEUtranCellFDDZteRepository>().To<ExternalEUtranCellFDDZteRepository>();

            ninjectKernel.Bind<IEUtranCellMeasurementZteRepository>().To<EUtranCellMeasurementZteRepository>();

            ninjectKernel.Bind<IEUtranCellFDDZteRepository>().To<EUtranCellFDDZteRepository>();

            ninjectKernel.Bind<IPrachFDDZteRepository>().To<PrachFDDZteRepository>();

            ninjectKernel.Bind<IPowerControlDLZteRepository>().To<PowerControlDLZteRepository>();

            ninjectKernel.Bind<ICellStasticRepository>().To<CellStasticRepository>();

            ninjectKernel.Bind<IPDSCHCfgRepository>().To<PDSCHCfgRepository>();

            ninjectKernel.Bind<ICellDlpcPdschPaRepository>().To<CellDlpcPdschPaRepository>();

            ninjectKernel.Bind<IFlowHuaweiRepository>().To<FlowHuaweiRepository>();

            ninjectKernel.Bind<IFlowZteRepository>().To<FlowZteRepository>();

            ninjectKernel.Bind<ICellStatMysqlRepository>().To<CellStatMysqlRepository>();

            ninjectKernel.Bind<CdmaRegionStatService>().ToSelf();

            ninjectKernel.Bind<CollegeStatService>().ToSelf();

            ninjectKernel.Bind<ENodebQueryService>().ToSelf();

            ninjectKernel.Bind<BtsQueryService>().ToSelf();

            ninjectKernel.Bind<CollegeENodebService>().ToSelf();

            ninjectKernel.Bind<CollegeBtssService>().ToSelf();

            ninjectKernel.Bind<CellService>().ToSelf();

            ninjectKernel.Bind<CdmaCellService>().ToSelf();

            ninjectKernel.Bind<College3GTestService>().ToSelf();

            ninjectKernel.Bind<College4GTestService>().ToSelf();

            ninjectKernel.Bind<CollegeDistributionService>().ToSelf();

            ninjectKernel.Bind<CollegeKpiService>().ToSelf();

            ninjectKernel.Bind<CollegePreciseService>().ToSelf();

            ninjectKernel.Bind<CollegeCdmaCellsService>().ToSelf();

            ninjectKernel.Bind<CollegeCellsService>().ToSelf();

            ninjectKernel.Bind<CollegeAlarmService>().ToSelf();
            
            ninjectKernel.Bind<PreciseStatService>().ToSelf();

            ninjectKernel.Bind<TownQueryService>().ToSelf();

            ninjectKernel.Bind<KpiImportService>().ToSelf();

            ninjectKernel.Bind<PreciseRegionStatService>().ToSelf();

            ninjectKernel.Bind<PreciseImportService>().ToSelf();

            ninjectKernel.Bind<AlarmsService>().ToSelf();

            ninjectKernel.Bind<TopDrop2GService>().ToSelf();

            ninjectKernel.Bind<TopConnection3GService>().ToSelf();

            ninjectKernel.Bind<BasicImportService>().ToSelf();

            ninjectKernel.Bind<ENodebDumpService>().ToSelf();

            ninjectKernel.Bind<BtsDumpService>().ToSelf();

            ninjectKernel.Bind<CellDumpService>().ToSelf();

            ninjectKernel.Bind<CdmaCellDumpService>().ToSelf();

            ninjectKernel.Bind<AreaTestDateService>().ToSelf();

            ninjectKernel.Bind<CsvFileInfoService>().ToSelf();

            ninjectKernel.Bind<RasterInfoService>().ToSelf();

            ninjectKernel.Bind<LteNeighborCellService>().ToSelf();

            ninjectKernel.Bind<NearestPciCellService>().ToSelf();

            ninjectKernel.Bind<WorkItemService>().ToSelf();

            ninjectKernel.Bind<InterferenceMatrixService>().ToSelf();

            ninjectKernel.Bind<NeighborMonitorService>().ToSelf();

            ninjectKernel.Bind<InterferenceNeighborService>().ToSelf();

            ninjectKernel.Bind<CellHuaweiMongoService>().ToSelf();

            ninjectKernel.Bind<IntraFreqHoService>().ToSelf();

            ninjectKernel.Bind<InterFreqHoService>().ToSelf();

            ninjectKernel.Bind<NeighborCellMongoService>().ToSelf();

            ninjectKernel.Bind<CellStasticService>().ToSelf();

            ninjectKernel.Bind<CellPowerService>().ToSelf();

            ninjectKernel.Bind<FlowService>().ToSelf();
        }
    }
}
