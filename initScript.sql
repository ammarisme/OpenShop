INSERT INTO [dbo].[IdentityRole] ([Id], [Name]) VALUES (N'1', N'Manager') ;
INSERT INTO [dbo].[ApplicationUser] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'd9d9ca43-1ef1-468b-b20b-822e4776207f', N'm@enterprise.com', 0, N'ADKVDbDNXLiR2deCgJi22LTjcbogcro4+4GxhwuTsDMj+7cjGFGPo1vZNyhRa/eyqA==', N'1e6039e2-9a89-4fe9-afc4-d3298442728b', NULL, 0, 0, NULL, 0, 0, N'm@enterprise.com') ;
INSERT INTO [dbo].[IdentityUserRole] ([UserId], [RoleId], [IdentityRole_Id], [ApplicationUser_Id]) VALUES (N'd9d9ca43-1ef1-468b-b20b-822e4776207f', N'1', NULL, NULL) ;
INSERT INTO [dbo].[Accounts] ([Id], [RetailerName], [RetailerAddress], [Rating], [BusinessPhoneNumber], [Status], [BRCNumber], [Category], [Currency]) VALUES (N'd9d9ca43-1ef1-468b-b20b-822e4776207f', N'1', N'1', 0, N'1', N'registered', N'1', N'1', NULL)




