USE AdepFramework

--Staff
Print 'Inserting Staff'
INSERT INTO Staff ([Code], [SecurityLabel], [StaffNumber], [LastName], [FirstName], [GradeCode], [IsActive], [RowIdentifier]) VALUES ('DF8752EF-74DB-4AEE-A55A-1D3D5F6AA6FE', '00000000-0000-0000-0000-000000000000', '00000001', 'User 1', 'Test', '9021DCC7-073F-4DF4-A305-87883EB2F7F3', 1, NULL)
INSERT INTO Staff ([Code], [SecurityLabel], [StaffNumber], [LastName], [FirstName], [GradeCode], [IsActive], [RowIdentifier]) VALUES ('F5174289-ACCE-43CE-8BF9-00EABF3DEF8E', '00000000-0000-0000-0000-000000000000', '00000002', 'User 2', 'Test', '03A59689-E75F-42E2-9F91-24FEF1FBAC4B', 1, NULL)
   
--Staff Attributes
Print 'Inserting Staff Attributes'
--Agent
INSERT INTO StaffAttributes ([Code], [SecurityLabel], [StaffCode], [ApplicationCode], [ApplicationAttributeCode], [LookupValue], [IsActive], [RowIdentifier]) VALUES ('56FC4AB7-3233-4691-91C5-470E35430DCE', '00000000-0000-0000-0000-000000000000', 'DF8752EF-74DB-4AEE-A55A-1D3D5F6AA6FE', '07653ABF-406F-43BC-B41A-C2FFA727138C', '1082B21F-FAB8-4CBD-A880-28E57A4C3574', 'Yes', 1, NULL)
INSERT INTO StaffAttributes ([Code], [SecurityLabel], [StaffCode], [ApplicationCode], [ApplicationAttributeCode], [LookupValue], [IsActive], [RowIdentifier]) VALUES ('4F0D0B1F-01E5-4A28-810C-07FBB9ADE797', '00000000-0000-0000-0000-000000000000', 'F5174289-ACCE-43CE-8BF9-00EABF3DEF8E', '07653ABF-406F-43BC-B41A-C2FFA727138C', '1082B21F-FAB8-4CBD-A880-28E57A4C3574', 'Yes', 1, NULL)

--Admin
INSERT INTO StaffAttributes ([Code], [SecurityLabel], [StaffCode], [ApplicationCode], [ApplicationAttributeCode], [LookupValue], [IsActive], [RowIdentifier]) VALUES ('88EFB420-8764-448C-8050-EBFF406ADEE9', '00000000-0000-0000-0000-000000000000', 'DF8752EF-74DB-4AEE-A55A-1D3D5F6AA6FE', '07653ABF-406F-43BC-B41A-C2FFA727138C', '019B28FE-D49D-4696-B51E-E597FBF0F96E', 'Yes', 1, NULL)
INSERT INTO StaffAttributes ([Code], [SecurityLabel], [StaffCode], [ApplicationCode], [ApplicationAttributeCode], [LookupValue], [IsActive], [RowIdentifier]) VALUES ('0B9EA497-30E2-468C-BB98-EBF7F616B9A0', '00000000-0000-0000-0000-000000000000', 'F5174289-ACCE-43CE-8BF9-00EABF3DEF8E', '07653ABF-406F-43BC-B41A-C2FFA727138C', '019B28FE-D49D-4696-B51E-E597FBF0F96E', 'Yes', 1, NULL)

--Reporter
INSERT INTO StaffAttributes ([Code], [SecurityLabel], [StaffCode], [ApplicationCode], [ApplicationAttributeCode], [LookupValue], [IsActive], [RowIdentifier]) VALUES ('FF711511-A60B-4C69-9B97-AF3B70D219D5', '00000000-0000-0000-0000-000000000000', 'DF8752EF-74DB-4AEE-A55A-1D3D5F6AA6FE', '07653ABF-406F-43BC-B41A-C2FFA727138C', 'BC17F8CD-F976-466A-9763-122CBC4E3F45', 'Yes', 1, NULL)
INSERT INTO StaffAttributes ([Code], [SecurityLabel], [StaffCode], [ApplicationCode], [ApplicationAttributeCode], [LookupValue], [IsActive], [RowIdentifier]) VALUES ('A66B08F8-B1E4-401D-BF1A-E73C745C8124', '00000000-0000-0000-0000-000000000000', 'F5174289-ACCE-43CE-8BF9-00EABF3DEF8E', '07653ABF-406F-43BC-B41A-C2FFA727138C', 'BC17F8CD-F976-466A-9763-122CBC4E3F45', 'Yes', 1, NULL)


--Organisations
Print 'Inserting Organisations'
SET IDENTITY_INSERT Organisation ON
INSERT INTO Organisation ([Code], [ID], [Name], [OrganisationTypeCode], [HEO], [DateDeleted], [IsActive], [RowIdentifier]) VALUES ('71C1581D-2518-4073-87F0-0058763EF39D', 438, 'BCAS Team 1', '4A772D26-8DFC-4605-B9A5-FB3A7ADC19F3', NULL, NULL, 1, NULL)
INSERT INTO Organisation ([Code], [ID], [Name], [OrganisationTypeCode], [HEO], [DateDeleted], [IsActive], [RowIdentifier]) VALUES ('231D4A11-117C-48E3-9AD0-78E58BEB980B', 443, 'BCAS Team 2', '4A772D26-8DFC-4605-B9A5-FB3A7ADC19F3', NULL, NULL, 1, NULL)
SET IDENTITY_INSERT Organisation OFF

--Organisation Hierarchies
Print 'Inserting OrganisationHierarchies'
INSERT INTO OrganisationHierarchy ([Code], [AncestorOrganisationCode], [OrganisationCode], [ImmediateParent], [HopsBetweenOrgAndAncestor], [IsActive], [RowIdentifier]) VALUES ('48F31BC1-16F3-4649-B7C6-675618E51C8C', '231D4A11-117C-48E3-9AD0-78E58BEB980B', '231D4A11-117C-48E3-9AD0-78E58BEB980B', 0, 0, 1, NULL)
INSERT INTO OrganisationHierarchy ([Code], [AncestorOrganisationCode], [OrganisationCode], [ImmediateParent], [HopsBetweenOrgAndAncestor], [IsActive], [RowIdentifier]) VALUES ('52B63918-E7A4-4D66-95FA-8BEC35521CBF', 'FB7BE9EC-D95B-4D29-9D47-FFEDEC64687B', '231D4A11-117C-48E3-9AD0-78E58BEB980B', 1, 1, 1, NULL)
INSERT INTO OrganisationHierarchy ([Code], [AncestorOrganisationCode], [OrganisationCode], [ImmediateParent], [HopsBetweenOrgAndAncestor], [IsActive], [RowIdentifier]) VALUES ('06FCEE24-6F8F-4F8D-8692-90F20BC2BDE7', '71C1581D-2518-4073-87F0-0058763EF39D', '71C1581D-2518-4073-87F0-0058763EF39D', 0, 0, 1, NULL)
INSERT INTO OrganisationHierarchy ([Code], [AncestorOrganisationCode], [OrganisationCode], [ImmediateParent], [HopsBetweenOrgAndAncestor], [IsActive], [RowIdentifier]) VALUES ('62259D50-8A20-49D1-9F0A-F0DE8395C8A1', 'FB7BE9EC-D95B-4D29-9D47-FFEDEC64687B', '71C1581D-2518-4073-87F0-0058763EF39D', 1, 1, 1, NULL)

--Staff Organisations
Print 'Inserting Staff Organisations'
INSERT INTO StaffOrganisation ([Code],[StaffCode],[OrganisationCode],[ApplicationCode],[IsDefault] ,[IsCurrent]) VALUES ('32A1E230-7A0D-4BFB-AE49-8068AC597D53','DF8752EF-74DB-4AEE-A55A-1D3D5F6AA6FE','71C1581D-2518-4073-87F0-0058763EF39D','07653ABF-406F-43BC-B41A-C2FFA727138C',1,1)
INSERT INTO StaffOrganisation ([Code],[StaffCode],[OrganisationCode],[ApplicationCode],[IsDefault] ,[IsCurrent]) VALUES ('6D27A47A-9D04-4903-8865-86A1D168414D','F5174289-ACCE-43CE-8BF9-00EABF3DEF8E','231D4A11-117C-48E3-9AD0-78E58BEB980B','07653ABF-406F-43BC-B41A-C2FFA727138C',1,1)


Print 'Finished Successfully'