﻿// <auto-generated />
using System;
using JiangDuo.EntityFramework.Core.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JiangDuo.Database.Migrations.Migrations
{
    [DbContext(typeof(MysqlDbContext))]
    [Migration("20220707133408_v1")]
    partial class v1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("JiangDuo.Core.Models.SysConfig", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("ConfigKey")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ConfigName")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ConfigValue")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("Creator")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Remark")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("Updater")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("sys_config", (string)null);
                });

            modelBuilder.Entity("JiangDuo.Core.Models.SysDept", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Ancestors")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("Creator")
                        .HasColumnType("bigint");

                    b.Property<string>("DeptCode")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("DeptName")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Leader")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<long?>("ParentId")
                        .HasColumnType("bigint");

                    b.Property<string>("Phone")
                        .HasMaxLength(11)
                        .HasColumnType("varchar(11)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("Updater")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("sys_dept", (string)null);
                });

            modelBuilder.Entity("JiangDuo.Core.Models.SysDict", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("Creator")
                        .HasColumnType("bigint");

                    b.Property<string>("DictName")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("DictType")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Remark")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("Updater")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("sys_dict", (string)null);
                });

            modelBuilder.Entity("JiangDuo.Core.Models.SysDictItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("Creator")
                        .HasColumnType("bigint");

                    b.Property<bool?>("IsDefault")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Label")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("Remark")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<long>("SysDictId")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("Updater")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("SysDictId");

                    b.ToTable("sys_dictitem", (string)null);
                });

            modelBuilder.Entity("JiangDuo.Core.Models.SysLogininfor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Browser")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Ipaddr")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("LoginLocation")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("LoginTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Msg")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Os")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("sys_logininfor", (string)null);
                });

            modelBuilder.Entity("JiangDuo.Core.Models.SysMenu", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Code")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("Creator")
                        .HasColumnType("bigint");

                    b.Property<bool?>("Hide")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Href")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Icon")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool?>("IsFrame")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool?>("KeepAlive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<long?>("ParentId")
                        .HasColumnType("bigint");

                    b.Property<string>("Path")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Remark")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("Updater")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("sys_menu", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedTime = new DateTimeOffset(new DateTime(2022, 7, 7, 21, 34, 8, 24, DateTimeKind.Unspecified).AddTicks(1051), new TimeSpan(0, 8, 0, 0, 0)),
                            Creator = 0L,
                            Icon = "carbon:dashboard",
                            IsDeleted = false,
                            Name = "dashboard",
                            Order = 0,
                            Path = "/dashboard",
                            Title = "首页",
                            Type = 0
                        },
                        new
                        {
                            Id = 2L,
                            CreatedTime = new DateTimeOffset(new DateTime(2022, 7, 7, 21, 34, 8, 24, DateTimeKind.Unspecified).AddTicks(1075), new TimeSpan(0, 8, 0, 0, 0)),
                            Creator = 0L,
                            Icon = "carbon:dashboard",
                            IsDeleted = false,
                            Name = "system",
                            Order = 1,
                            Path = "/system",
                            Title = "系统管理",
                            Type = 0
                        },
                        new
                        {
                            Id = 3L,
                            CreatedTime = new DateTimeOffset(new DateTime(2022, 7, 7, 21, 34, 8, 24, DateTimeKind.Unspecified).AddTicks(1080), new TimeSpan(0, 8, 0, 0, 0)),
                            Creator = 0L,
                            Icon = "carbon:dashboard",
                            IsDeleted = false,
                            Name = "system_peopleConfig_user",
                            Order = 1,
                            ParentId = 2L,
                            Path = "/system/peopleConfig/user",
                            Title = "用户管理",
                            Type = 0
                        },
                        new
                        {
                            Id = 4L,
                            CreatedTime = new DateTimeOffset(new DateTime(2022, 7, 7, 21, 34, 8, 24, DateTimeKind.Unspecified).AddTicks(1082), new TimeSpan(0, 8, 0, 0, 0)),
                            Creator = 0L,
                            Icon = "carbon:dashboard",
                            IsDeleted = false,
                            Name = "system_peopleConfig_role",
                            Order = 2,
                            ParentId = 2L,
                            Path = "/system/peopleConfig/role",
                            Title = "角色管理",
                            Type = 0
                        },
                        new
                        {
                            Id = 5L,
                            CreatedTime = new DateTimeOffset(new DateTime(2022, 7, 7, 21, 34, 8, 24, DateTimeKind.Unspecified).AddTicks(1084), new TimeSpan(0, 8, 0, 0, 0)),
                            Creator = 0L,
                            Icon = "carbon:dashboard",
                            IsDeleted = false,
                            Name = "system_menu",
                            Order = 3,
                            ParentId = 2L,
                            Path = "/system/menu",
                            Title = "菜单管理",
                            Type = 0
                        },
                        new
                        {
                            Id = 6L,
                            CreatedTime = new DateTimeOffset(new DateTime(2022, 7, 7, 21, 34, 8, 24, DateTimeKind.Unspecified).AddTicks(1086), new TimeSpan(0, 8, 0, 0, 0)),
                            Creator = 0L,
                            Icon = "carbon:dashboard",
                            IsDeleted = false,
                            Name = "system_dept",
                            Order = 5,
                            ParentId = 2L,
                            Path = "/system/dept",
                            Title = "部门管理",
                            Type = 0
                        },
                        new
                        {
                            Id = 7L,
                            CreatedTime = new DateTimeOffset(new DateTime(2022, 7, 7, 21, 34, 8, 24, DateTimeKind.Unspecified).AddTicks(1088), new TimeSpan(0, 8, 0, 0, 0)),
                            Creator = 0L,
                            Icon = "carbon:dashboard",
                            IsDeleted = false,
                            Name = "system_post",
                            Order = 6,
                            ParentId = 2L,
                            Path = "/system/post",
                            Title = "岗位管理",
                            Type = 0
                        },
                        new
                        {
                            Id = 8L,
                            CreatedTime = new DateTimeOffset(new DateTime(2022, 7, 7, 21, 34, 8, 24, DateTimeKind.Unspecified).AddTicks(1089), new TimeSpan(0, 8, 0, 0, 0)),
                            Creator = 0L,
                            Icon = "carbon:dashboard",
                            IsDeleted = false,
                            Name = "system_dict",
                            Order = 7,
                            ParentId = 2L,
                            Path = "/system/dict",
                            Title = "字典管理",
                            Type = 0
                        });
                });

            modelBuilder.Entity("JiangDuo.Core.Models.SysNotice", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("Creator")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("NoticeContent")
                        .HasColumnType("longtext");

                    b.Property<string>("NoticeTitle")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Remark")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("Updater")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("sys_notice", (string)null);
                });

            modelBuilder.Entity("JiangDuo.Core.Models.SysOperLog", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("ErrorMsg")
                        .HasColumnType("longtext");

                    b.Property<string>("JsonResult")
                        .HasColumnType("longtext");

                    b.Property<string>("Method")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("OperIp")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("OperName")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("OperParam")
                        .HasColumnType("longtext");

                    b.Property<string>("OperSource")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTimeOffset>("OperTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("OperUrl")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RequestMethod")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("StackTrace")
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("sys_operLog", (string)null);
                });

            modelBuilder.Entity("JiangDuo.Core.Models.SysPost", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("Creator")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("PostCode")
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("PostName")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Remark")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("Updater")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("sys_post", (string)null);
                });

            modelBuilder.Entity("JiangDuo.Core.Models.SysRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("Creator")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Remark")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("RoleName")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("Updater")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("sys_role", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedTime = new DateTimeOffset(new DateTime(2022, 7, 7, 21, 34, 8, 26, DateTimeKind.Unspecified).AddTicks(8183), new TimeSpan(0, 8, 0, 0, 0)),
                            Creator = 0L,
                            IsDeleted = false,
                            RoleName = "管理员",
                            Status = 0
                        });
                });

            modelBuilder.Entity("JiangDuo.Core.Models.SysRoleMenu", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("MenuId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("sys_role_menu", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            MenuId = 1L,
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            MenuId = 2L,
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 3L,
                            MenuId = 3L,
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 4L,
                            MenuId = 4L,
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 5L,
                            MenuId = 5L,
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 6L,
                            MenuId = 6L,
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 7L,
                            MenuId = 7L,
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 8L,
                            MenuId = 8L,
                            RoleId = 1L
                        });
                });

            modelBuilder.Entity("JiangDuo.Core.Models.SysUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Avatar")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("Creator")
                        .HasColumnType("bigint");

                    b.Property<long>("DeptId")
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("LoginDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LoginIp")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("NickName")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PassWord")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Phonenumber")
                        .HasMaxLength(11)
                        .HasColumnType("varchar(11)");

                    b.Property<string>("Remark")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<int>("Sex")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("Updater")
                        .HasColumnType("bigint");

                    b.Property<string>("UserName")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("sys_user", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedTime = new DateTimeOffset(new DateTime(2022, 7, 7, 21, 34, 8, 28, DateTimeKind.Unspecified).AddTicks(1771), new TimeSpan(0, 8, 0, 0, 0)),
                            Creator = 0L,
                            DeptId = 0L,
                            IsDeleted = false,
                            NickName = "管理员",
                            PassWord = "9B6F539B39186126518612B27FB8504A",
                            Sex = 0,
                            Status = 0,
                            Type = 0,
                            UserName = "admin"
                        });
                });

            modelBuilder.Entity("JiangDuo.Core.Models.SysUserPost", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("PostId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("sys_user_post", (string)null);
                });

            modelBuilder.Entity("JiangDuo.Core.Models.SysUserRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("sys_user_role", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            RoleId = 1L,
                            UserId = 1L
                        });
                });

            modelBuilder.Entity("JiangDuo.Core.Models.UploadFile", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("Creator")
                        .HasColumnType("bigint");

                    b.Property<string>("FileExt")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<long>("FileLength")
                        .HasColumnType("bigint");

                    b.Property<string>("FileName")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("FilePath")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<int>("FileSource")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("OldName")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("Updater")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("uploadfile", (string)null);
                });

            modelBuilder.Entity("JiangDuo.Core.Models.SysDept", b =>
                {
                    b.HasOne("JiangDuo.Core.Models.SysDept", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("JiangDuo.Core.Models.SysDictItem", b =>
                {
                    b.HasOne("JiangDuo.Core.Models.SysDict", null)
                        .WithMany("SysDictItem")
                        .HasForeignKey("SysDictId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JiangDuo.Core.Models.SysDict", b =>
                {
                    b.Navigation("SysDictItem");
                });
#pragma warning restore 612, 618
        }
    }
}
