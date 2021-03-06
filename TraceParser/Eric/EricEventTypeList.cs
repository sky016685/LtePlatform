﻿using System.Collections;

namespace TraceParser.Eric
{
    public class EricEventTypeList
    {
        public EricEventTypeList()
        {
            ht_EricEventType = new Hashtable
            {
                {0, "RRC_RRC_CONNECTION_SETUP"},
                {1, "RRC_RRC_CONNECTION_REJECT"},
                {2, "RRC_RRC_CONNECTION_REQUEST"},
                {3, "RRC_RRC_CONNECTION_RE_ESTABLISHMENT_REQUEST"},
                {4, "RRC_RRC_CONNECTION_RE_ESTABLISHMENT_REJECT"},
                {5, "RRC_RRC_CONNECTION_RELEASE"},
                {6, "RRC_DL_INFORMATION_TRANSFER"},
                {8, "RRC_RRC_CONNECTION_RECONFIGURATION"},
                {9, "RRC_SECURITY_MODE_COMMAND"},
                {10, "RRC_UE_CAPABILITY_ENQUIRY"},
                {11, "RRC_MEASUREMENT_REPORT"},
                {12, "RRC_RRC_CONNECTION_SETUP_COMPLETE"},
                {13, "RRC_RRC_CONNECTION_RECONFIGURATION_COMPLETE"},
                {0x10, "RRC_UL_INFORMATION_TRANSFER"},
                {0x11, "RRC_SECURITY_MODE_COMPLETE"},
                {0x12, "RRC_SECURITY_MODE_FAILURE"},
                {0x13, "RRC_UE_CAPABILITY_INFORMATION"},
                {0x15, "RRC_MASTER_INFORMATION_BLOCK"},
                {0x16, "RRC_SYSTEM_INFORMATION"},
                {0x17, "RRC_SYSTEM_INFORMATION_BLOCK_TYPE_1"},
                {0x18, "RRC_CONNECTION_RE_ESTABLISHMENT"},
                {0x19, "RRC_CONNECTION_RE_ESTABLISHMENT_COMPLETE"},
                {0x1a, "RRC_UE_INFORMATION_RESPONSE"},
                {0x1b, "RRC_UE_INFORMATION_REQUEST"},
                {0x1c, "RRC_MBSFNAREA_CONFIGURATION"},
                {0x1d, "RRC_CSFB_PARAMETERS_REQUEST_CDMA2000"},
                {30, "RRC_CSFB_PARAMETERS_RESPONSE_CDMA2000"},
                {0x1f, "RRC_HANDOVER_FROM_EUTRA_PREPARATION_REQUEST"},
                {0x20, "RRC_UL_HANDOVER_PREPARATION_TRANSFER"},
                {0x400, "S1_DOWNLINK_S1_CDMA2000_TUNNELING"},
                {0x401, "S1_DOWNLINK_NAS_TRANSPORT"},
                {0x402, "S1_ENB_STATUS_TRANSFER"},
                {0x403, "S1_ERROR_INDICATION"},
                {0x404, "S1_HANDOVER_CANCEL"},
                {0x405, "S1_HANDOVER_CANCEL_ACKNOWLEDGE"},
                {0x406, "S1_HANDOVER_COMMAND"},
                {0x407, "S1_HANDOVER_FAILURE"},
                {0x408, "S1_HANDOVER_NOTIFY"},
                {0x409, "S1_HANDOVER_PREPARATION_FAILURE"},
                {0x40a, "S1_HANDOVER_REQUEST"},
                {0x40b, "S1_HANDOVER_REQUEST_ACKNOWLEDGE"},
                {0x40c, "S1_HANDOVER_REQUIRED"},
                {0x40d, "S1_INITIAL_CONTEXT_SETUP_FAILURE"},
                {0x40e, "S1_INITIAL_CONTEXT_SETUP_REQUEST"},
                {0x40f, "S1_INITIAL_CONTEXT_SETUP_RESPONSE"},
                {0x410, "S1_INITIAL_UE_MESSAGE"},
                {0x411, "S1_MME_STATUS_TRANSFER"},
                {0x412, "S1_NAS_NON_DELIVERY_INDICATION"},
                {0x413, "S1_PAGING"},
                {0x414, "S1_PATH_SWITCH_REQUEST"},
                {0x415, "S1_PATH_SWITCH_REQUEST_ACKNOWLEDGE"},
                {0x416, "S1_PATH_SWITCH_REQUEST_FAILURE"},
                {0x417, "S1_RESET"},
                {0x418, "S1_RESET_ACKNOWLEDGE"},
                {0x419, "S1_ERAB_MODIFY_REQUEST"},
                {0x41a, "S1_ERAB_MODIFY_RESPONSE"},
                {0x41b, "S1_ERAB_RELEASE_COMMAND"},
                {0x41c, "S1_ERAB_RELEASE_RESPONSE"},
                {0x41d, "S1_ERAB_RELEASE_REQUEST"},
                {0x41e, "S1_ERAB_SETUP_REQUEST"},
                {0x41f, "S1_ERAB_SETUP_RESPONSE"},
                {0x420, "S1_S1_SETUP_FAILURE"},
                {0x421, "S1_S1_SETUP_REQUEST"},
                {0x422, "S1_S1_SETUP_RESPONSE"},
                {0x423, "S1_UE_CAPABILITY_INFO_INDICATION"},
                {0x424, "S1_UE_CONTEXT_MODIFICATION_FAILURE"},
                {0x425, "S1_UE_CONTEXT_MODIFICATION_REQUEST"},
                {0x426, "S1_UE_CONTEXT_MODIFICATION_RESPONSE"},
                {0x427, "S1_UE_CONTEXT_RELEASE_COMMAND"},
                {0x428, "S1_UE_CONTEXT_RELEASE_COMPLETE"},
                {0x429, "S1_UE_CONTEXT_RELEASE_REQUEST"},
                {0x42a, "S1_UPLINK_S1_CDMA2000_TUNNELING"},
                {0x42b, "S1_UPLINK_NAS_TRANSPORT"},
                {0x42c, "S1_ENB_CONFIGURATION_UPDATE"},
                {0x42d, "S1_ENB_CONFIGURATION_UPDATE_ACKNOWLEDGE"},
                {0x42e, "S1_ENB_CONFIGURATION_UPDATE_FAILURE"},
                {0x42f, "S1_MME_CONFIGURATION_UPDATE"},
                {0x430, "S1_MME_CONFIGURATION_UPDATE_ACKNOWLEDGE"},
                {0x431, "S1_MME_CONFIGURATION_UPDATE_FAILURE"},
                {0x432, "S1_ENB_DIRECT_INFORMATION_TRANSFER"},
                {0x433, "S1_MME_DIRECT_INFORMATION_TRANSFER"},
                {0x434, "S1_WRITE_REPLACE_WARNING_REQUEST"},
                {0x435, "S1_WRITE_REPLACE_WARNING_RESPONSE"},
                {0x436, "S1_KILL_REQUEST"},
                {0x437, "S1_KILL_RESPONSE"},
                {0x438, "S1_ERAB_RELEASE_INDICATION"},
                {0x439, "S1_DOWNLINK_UE_ASSOCIATED_LPPA_TRANSPORT"},
                {0x43a, "S1_UPLINK_UE_ASSOCIATED_LPPA_TRANSPORT"},
                {0x43b, "S1_DOWNLINK_NON_UE_ASSOCIATED_LPPA_TRANSPORT"},
                {0x43c, "S1_UPLINK_NON_UE_ASSOCIATED_LPPA_TRANSPORT"},
                {0x800, "X2_RESET_REQUEST"},
                {0x801, "X2_RESET_RESPONSE"},
                {0x802, "X2_X2_SETUP_REQUEST"},
                {0x803, "X2_X2_SETUP_RESPONSE"},
                {0x804, "X2_ERROR_INDICATION"},
                {0x805, "X2_ENB_CONFIGURATION_UPDATE"},
                {0x806, "X2_ENB_CONFIGURATION_UPDATE_ACKNOWLEDGE"},
                {0x807, "X2_ENB_CONFIGURATION_UPDATE_FAILURE"},
                {0x808, "X2_X2_SETUP_FAILURE"},
                {0x809, "X2_HANDOVER_CANCEL"},
                {0x80a, "X2_HANDOVER_REQUEST"},
                {0x80b, "X2_HANDOVER_REQUEST_ACKNOWLEDGE"},
                {0x80c, "X2_SN_STATUS_TRANSFER"},
                {0x80d, "X2_UE_CONTEXT_RELEASE"},
                {0x80e, "X2_HANDOVER_PREPARATION_FAILURE"},
                {0x80f, "S1_LOCATION_REPORTING_CONTROL"},
                {0x810, "S1_LOCATION_REPORT"},
                {0x811, "S1_LOCATION_REPORT_FAILURE_INDICATION"},
                {0x812, "S1_MME_CONFIGURATION_TRANSFER"},
                {0x813, "S1_ENB_CONFIGURATION_TRANSFER"},
                {0x814, "X2_PRIVATE_MESSAGE"},
                {0x815, "X2_RLF_INDICATION"},
                {0x816, "X2_HANDOVER_REPORT"},
                {0x817, "X2_CONTEXT_FETCH_REQUEST"},
                {0x818, "X2_CONTEXT_FETCH_RESPONSE"},
                {0x819, "X2_CONTEXT_FETCH_FAILURE"},
                {0x81a, "X2_CONTEXT_FETCH_RESPONSE_ACCEPT"},
                {0x81b, "X2_CONTEXT_FETCH_RESPONSE_REJECT"},
                {0xc00, "INTERNAL_PER_RADIO_UTILIZATION"},
                {0xc02, "INTERNAL_PER_UE_ACTIVE_SESSION_TIME"},
                {0xc03, "INTERNAL_PER_RADIO_UE_MEASUREMENT"},
                {0xc04, "INTERNAL_PER_UE_TRAFFIC_REP"},
                {0xc05, "INTERNAL_PER_UE_RB_TRAFFIC_REP"},
                {0xc07, "INTERNAL_PER_CELL_TRAFFIC_REPORT"},
                {0xc09, "INTERNAL_PER_RADIO_CELL_MEASUREMENT"},
                {0xc0c, "INTERNAL_PER_PROCESSOR_LOAD"},
                {0xc0d, "INTERNAL_PER_PRB_LICENSE_UTIL_REP"},
                {0xc0e, "INTERNAL_PER_CELL_QCI_TRAFFIC_REP"},
                {0xc0f, "INTERNAL_PER_UE_LCG_TRAFFIC_REP"},
                {0xc10, "INTERNAL_PER_RADIO_CELL_NOISE_INTERFERENCE_PRB"},
                {0xc11, "INTERNAL_PER_RADIO_CELL_CQI_SUBBAND"},
                {0xc12, "INTERNAL_PER_UETR_RADIO_UTILIZATION"},
                {0xc13, "INTERNAL_PER_UETR_UE_ACTIVE_SESSION_TIME"},
                {0xc14, "INTERNAL_PER_UETR_RADIO_UE_MEASUREMENT"},
                {0xc15, "INTERNAL_PER_UETR_UE_TRAFFIC_REP"},
                {0xc16, "INTERNAL_PER_UETR_UE_RB_TRAFFIC_REP"},
                {0xc17, "INTERNAL_PER_UETR_CELL_QCI_TRAFFIC_REP"},
                {0xc18, "INTERNAL_PER_UETR_UE_LCG_TRAFFIC_REP"},
                {0xc1a, "INTERNAL_PER_UETR_PRB_LICENSE_UTIL_REP"},
                {0xc1b, "INTERNAL_PER_UETR_CELL_TRAFFIC_REPORT"},
                {0xc1d, "INTERNAL_PER_UETR_RADIO_CELL_MEASUREMENT"},
                {0xc1e, "INTERNAL_PER_UETR_RADIO_CELL_NOISE_INTERFERENCE_PRB"},
                {0xc1f, "INTERNAL_PER_UETR_RADIO_CELL_CQI_SUBBAND"},
                {0xc22, "INTERNAL_PER_EVENT_ETWS_REPET_COMPL"},
                {0xc23, "INTERNAL_PER_EVENT_CMAS_REPET_COMPL"},
                {0xc24, "INTERNAL_PER_RADIO_UE_MEASUREMENT_TA"},
                {0x1001, "INTERNAL_PROC_RRC_CONN_SETUP"},
                {0x1002, "INTERNAL_PROC_S1_SIG_CONN_SETUP"},
                {0x1003, "INTERNAL_PROC_ERAB_SETUP"},
                {0x1006, "INTERNAL_PROC_HO_PREP_S1_OUT"},
                {0x1007, "INTERNAL_PROC_HO_PREP_S1_IN"},
                {0x1008, "INTERNAL_PROC_HO_EXEC_S1_OUT"},
                {0x1009, "INTERNAL_PROC_HO_EXEC_S1_IN"},
                {0x100a, "INTERNAL_PROC_INITIAL_CTXT_SETUP"},
                {0x100b, "INTERNAL_PROC_DNS_LOOKUP"},
                {0x100c, "INTERNAL_PROC_REVERSE_DNS_LOOKUP"},
                {0x100d, "INTERNAL_PROC_SCTP_SETUP"},
                {0x100e, "INTERNAL_PROC_HO_PREP_X2_OUT"},
                {0x100f, "INTERNAL_PROC_HO_PREP_X2_IN"},
                {0x1010, "INTERNAL_PROC_HO_EXEC_X2_OUT"},
                {0x1011, "INTERNAL_PROC_HO_EXEC_X2_IN"},
                {0x1012, "INTERNAL_PROC_ERAB_RELEASE"},
                {0x1014, "INTERNAL_PROC_S1_SETUP"},
                {0x1015, "INTERNAL_PROC_ANR_CGI_REPORT"},
                {0x1016, "INTERNAL_PROC_X2_SETUP"},
                {0x1017, "INTERNAL_PROC_S1_TENB_CONF_LOOKUP"},
                {0x1018, "INTERNAL_PROC_RRC_CONN_RECONF_NO_MOB"},
                {0x1019, "INTERNAL_PROC_RRC_CONNECTION_RE_ESTABLISHMENT"},
                {0x101a, "INTERNAL_PROC_ERAB_MODIFY"},
                {0x101b, "INTERNAL_PROC_X2_RESET"},
                {0x101c, "INTERNAL_PROC_SCTP_SHUTDOWN"},
                {0x101d, "INTERNAL_PROC_UE_CTXT_RELEASE"},
                {0x101e, "INTERNAL_PROC_UE_CTXT_MODIFY"},
                {0x1020, "INTERNAL_PROC_UE_CTXT_FETCH"},
                {0x1021, "INTERNAL_PROC_M3_SETUP"},
                {0x1022, "INTERNAL_PROC_MBMS_SESSION_START"},
                {0x1400, "INTERNAL_EVENT_RRC_ERROR"},
                {0x1403, "INTERNAL_EVENT_NO_RESET_ACK_FROM_MME"},
                {0x1404, "INTERNAL_EVENT_S1AP_PROTOCOL_ERROR"},
                {0x1407, "INTERNAL_EVENT_PM_RECORDING_FAULT_JVM"},
                {0x1408, "INTERNAL_EVENT_MAX_UETRACES_REACHED"},
                {0x140b, "INTERNAL_EVENT_UNEXPECTED_RRC_MSG"},
                {0x140d, "INTERNAL_EVENT_PM_EVENT_SUSPECTMARKED"},
                {0x140e, "INTERNAL_EVENT_INTEGRITY_VER_FAIL_RRC_MSG"},
                {0x1410, "INTERNAL_EVENT_X2_CONN_RELEASE"},
                {0x1411, "INTERNAL_EVENT_X2AP_PROTOCOL_ERROR"},
                {0x1412, "INTERNAL_EVENT_MAX_STORAGESIZE_REACHED"},
                {0x1413, "INTERNAL_EVENT_MAX_FILESIZE_REACHED"},
                {0x1414, "INTERNAL_EVENT_MAX_FILESIZE_RECOVERY"},
                {0x1417, "INTERNAL_EVENT_PM_DATA_COLLECTION_LOST"},
                {0x1418, "INTERNAL_EVENT_NEIGHBCELL_CHANGE"},
                {0x1419, "INTERNAL_EVENT_NEIGHBENB_CHANGE"},
                {0x141a, "INTERNAL_EVENT_NEIGHBREL_ADD"},
                {0x141b, "INTERNAL_EVENT_NEIGHBREL_REMOVE"},
                {0x141c, "INTERNAL_EVENT_UE_ANR_CONFIG_PCI"},
                {0x141d, "INTERNAL_EVENT_UE_ANR_PCI_REPORT"},
                {0x1421, "UE_MEAS_INTRAFREQ1"},
                {0x1422, "UE_MEAS_INTRAFREQ2"},
                {0x1423, "UE_MEAS_EVENT_FEAT_NOT_AVAIL"},
                {0x1424, "UE_MEAS_EVENT_NOT_CONFIG"},
                {0x1425, "INTERNAL_EVENT_UE_MEAS_FAILURE"},
                {0x1427, "INTERNAL_EVENT_ANR_CONFIG_MISSING"},
                {0x142a, "INTERNAL_EVENT_LIC_GRACE_PERIOD_STARTED"},
                {0x142b, "INTERNAL_EVENT_LIC_GRACE_PERIOD_RESET"},
                {0x142c, "INTERNAL_EVENT_LIC_GRACE_PERIOD_EXPIRED"},
                {0x142d, "INTERNAL_EVENT_LIC_GRACE_PERIOD_EXPIRING"},
                {0x142e, "INTERNAL_EVENT_LICENSE_UNAVAILABLE"},
                {0x142f, "INTERNAL_EVENT_EUTRAN_FREQUENCY_ADD"},
                {0x1430, "INTERNAL_EVENT_FREQ_REL_ADD"},
                {0x1432, "INTERNAL_EVENT_RECOMMENDED_NR_SI_UPDATES_REACHED"},
                {0x1433, "INTERNAL_EVENT_IP_ADDR_GET_FAILURE"},
                {0x1434, "INTERNAL_EVENT_UE_CAPABILITY"},
                {0x1435, "INTERNAL_EVENT_ANR_PCI_REPORT_WANTED"},
                {0x1436, "INTERNAL_EVENT_MEAS_CONFIG_A1"},
                {0x1437, "INTERNAL_EVENT_MEAS_CONFIG_A2"},
                {0x1438, "INTERNAL_EVENT_MEAS_CONFIG_A3"},
                {0x1439, "INTERNAL_EVENT_MEAS_CONFIG_A4"},
                {0x143a, "INTERNAL_EVENT_MEAS_CONFIG_A5"},
                {0x143b, "INTERNAL_EVENT_MEAS_CONFIG_PERIODICAL_EUTRA"},
                {0x143c, "INTERNAL_EVENT_MEAS_CONFIG_B2_GERAN"},
                {0x143d, "INTERNAL_EVENT_MEAS_CONFIG_B2_UTRA"},
                {0x143e, "INTERNAL_EVENT_MEAS_CONFIG_B2_CDMA2000"},
                {0x143f, "INTERNAL_EVENT_MEAS_CONFIG_PERIODICAL_GERAN"},
                {0x1440, "INTERNAL_EVENT_MEAS_CONFIG_PERIODICAL_UTRA"},
                {0x1441, "INTERNAL_UE_MEAS_ABORT"},
                {0x1448, "INTERNAL_EVENT_ONGOING_UE_MEAS"},
                {0x1449, "INTERNAL_EVENT_UE_MOBILITY_EVAL"},
                {0x144a, "INTERNAL_EVENT_NEIGHBCELL_ADDITIONAL_CGI"},
                {0x144b, "INTERNAL_EVENT_SON_UE_OSCILLATION_PREVENTED"},
                {0x144c, "INTERNAL_EVENT_SON_OSCILLATION_DETECTED"},
                {0x144d, "INTERNAL_EVENT_IMLB_CONTROL"},
                {0x144e, "INTERNAL_EVENT_IMLB_ACTION"},
                {0x144f, "INTERNAL_EVENT_UNKNOWN_UE_AT_RE_ESTABLISHMENT"},
                {0x1450, "INTERNAL_EVENT_SPID_PRIORITY_IGNORED"},
                {0x1451, "INTERNAL_EVENT_RIM_RAN_INFORMATION_RECEIVED"},
                {0x1452, "INTERNAL_EVENT_ERAB_DATA_INFO"},
                {0x1453, "INTERNAL_EVENT_TOO_EARLY_HO"},
                {0x1454, "INTERNAL_EVENT_TOO_LATE_HO"},
                {0x1455, "INTERNAL_EVENT_HO_WRONG_CELL"},
                {0x1456, "INTERNAL_EVENT_HO_WRONG_CELL_REEST"},
                {0x1457, "INTERNAL_EVENT_RRC_UE_INFORMATION"},
                {0x1458, "INTERNAL_EVENT_S1_NAS_NON_DELIVERY_INDICATION"},
                {0x1459, "INTERNAL_EVENT_S1_ERROR_INDICATION"},
                {0x145a, "INTERNAL_EVENT_X2_ERROR_INDICATION"},
                {0x145b, "INTERNAL_EVENT_ADMISSION_BLOCKING_STARTED"},
                {0x145c, "INTERNAL_EVENT_ADMISSION_BLOCKING_STOPPED"},
                {0x145d, "INTERNAL_EVENT_ADMISSION_BLOCKING_UPDATED"},
                {0x145e, "INTERNAL_EVENT_ERAB_ROHC_FAIL_LIC_REJECT"},
                {0x145f, "INTERNAL_EVENT_LB_INTER_FREQ"},
                {0x1460, "INTERNAL_EVENT_LB_QUAL_UE"},
                {0x1461, "INTERNAL_EVENT_PCI_CONFLICT_DETECTED"},
                {0x1462, "INTERNAL_EVENT_PCI_CONFLICT_RESOLVED"},
                {0x1463, "INTERNAL_EVENT_LB_MEAS_UE"},
                {0x1464, "INTERNAL_EVENT_LB_SUB_RATIO"},
                {0x1465, "INTERNAL_EVENT_ETWS_REQ"},
                {0x1466, "INTERNAL_EVENT_ETWS_RESP"},
                {0x1467, "INTERNAL_EVENT_CMAS_REQ"},
                {0x1468, "INTERNAL_EVENT_CMAS_RESP"},
                {0x1469, "INTERNAL_EVENT_ETWS_REPET_STOPPED"},
                {0x146a, "INTERNAL_EVENT_CMAS_REPET_STOPPED"},
                {0x146b, "INTERNAL_EVENT_UE_ANR_CONFIG_PCI_REMOVE"},
                {0x146c, "INTERNAL_EVENT_ADV_CELL_SUP_DETECTION"},
                {0x146d, "INTERNAL_EVENT_ADV_CELL_SUP_RECOVERY_ATTEMPT"},
                {0x146e, "INTERNAL_EVENT_ADV_CELL_SUP_RECOVERY_RESULT"},
                {0x1471, "INTERNAL_EVENT_LOAD_CONTROL_STATE_TRANSITION"},
                {0x1472, "INTERNAL_EVENT_MEAS_CONFIG_B1_UTRA"},
                {0x1473, "INTERNAL_EVENT_MEAS_CONFIG_B1_CDMA2000"},
                {0x2000, "M3_M3_SETUP_REQUEST"},
                {0x2001, "M3_M3_SETUP_RESPONSE"},
                {0x2002, "M3_M3_SETUP_FAILURE"},
                {0x2003, "M3_MBMS_SESSION_START_REQUEST"},
                {0x2004, "M3_MBMS_SESSION_START_RESPONSE"},
                {0x2005, "M3_MBMS_SESSION_START_FAILURE"},
                {0x2006, "M3_MBMS_SESSION_STOP_REQUEST"},
                {0x2007, "M3_MBMS_SESSION_STOP_RESPONSE"},
                {0x2008, "M3_RESET"},
                {0x2009, "M3_MCE_CONFIGURATION_UPDATE_REQUEST"},
                {0x200a, "M3_MCE_CONFIGURATION_UPDATE_RESPONSE"},
                {0x200b, "M3_MCE_CONFIGURATION_UPDATE_FAILURE"}
            };
        }

        public Hashtable ht_EricEventType { get; set; }
    }
}
