


/*******************************************************************************
* Copyright (C)  JuCheap.Com
* 
* Author: dj.wong
* Create Date: 2015/8/7 11:12:12
* Description: Automated building by service@JuCheap.com 
* 
* Revision History:
* Date         Author               Description
*
*********************************************************************************/

using AutoMapper;
using JuCheap.Entity;
using JuCheap.Service.Dto;

namespace JuCheap.Service
{
    /// <summary>
    /// AutoMapper 配置
    /// </summary>
    public partial class AutoMapperConfiguration
    {
        /// <summary>
        /// 配置AutoMapper
        /// </summary>
        public static void Config()
        {

			Mapper.CreateMap<DCL_DataEntity, DCL_DataDto>();
			Mapper.CreateMap<DCL_DataDto, DCL_DataEntity>();

			Mapper.CreateMap<DictionariesEntity, DictionariesDto>();
			Mapper.CreateMap<DictionariesDto, DictionariesEntity>();

			Mapper.CreateMap<EmailPoolEntity, EmailPoolDto>();
			Mapper.CreateMap<EmailPoolDto, EmailPoolEntity>();

			Mapper.CreateMap<EmailReceiverEntity, EmailReceiverDto>();
			Mapper.CreateMap<EmailReceiverDto, EmailReceiverEntity>();

			Mapper.CreateMap<HanderDataForXF_KZEntity, HanderDataForXF_KZDto>();
			Mapper.CreateMap<HanderDataForXF_KZDto, HanderDataForXF_KZEntity>();

			Mapper.CreateMap<HanderDataForXF_SYEntity, HanderDataForXF_SYDto>();
			Mapper.CreateMap<HanderDataForXF_SYDto, HanderDataForXF_SYEntity>();

			Mapper.CreateMap<LoginLogEntity, LoginLogDto>();
			Mapper.CreateMap<LoginLogDto, LoginLogEntity>();

			Mapper.CreateMap<MenuEntity, MenuDto>();
			Mapper.CreateMap<MenuDto, MenuEntity>();

			Mapper.CreateMap<PageViewEntity, PageViewDto>();
			Mapper.CreateMap<PageViewDto, PageViewEntity>();

			Mapper.CreateMap<RoleEntity, RoleDto>();
			Mapper.CreateMap<RoleDto, RoleEntity>();

			Mapper.CreateMap<RoleMenuEntity, RoleMenuDto>();
			Mapper.CreateMap<RoleMenuDto, RoleMenuEntity>();

			Mapper.CreateMap<SleeveEntity, SleeveDto>();
			Mapper.CreateMap<SleeveDto, SleeveEntity>();

			Mapper.CreateMap<UserEntity, UserDto>();
			Mapper.CreateMap<UserDto, UserEntity>();

			Mapper.CreateMap<UserRoleEntity, UserRoleDto>();
			Mapper.CreateMap<UserRoleDto, UserRoleEntity>();

			Mapper.CreateMap<XF_KZ_CodeSizeEntity, XF_KZ_CodeSizeDto>();
			Mapper.CreateMap<XF_KZ_CodeSizeDto, XF_KZ_CodeSizeEntity>();

			Mapper.CreateMap<XF_SY_NAN_ChiMaEntity, XF_SY_NAN_ChiMaDto>();
			Mapper.CreateMap<XF_SY_NAN_ChiMaDto, XF_SY_NAN_ChiMaEntity>();

			Mapper.CreateMap<XF_SY_NU_CodeSizeEntity, XF_SY_NU_CodeSizeDto>();
			Mapper.CreateMap<XF_SY_NU_CodeSizeDto, XF_SY_NU_CodeSizeEntity>();

        }
    }
}
