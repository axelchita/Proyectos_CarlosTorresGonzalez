﻿CREATE TABLE [dbo].[PCCKeys] (
    [IDPCC]   INT        IDENTITY (1, 1) NOT NULL,
    [PCCCode] NCHAR (10) NULL,
    CONSTRAINT [PK_PCCKeys] PRIMARY KEY CLUSTERED ([IDPCC] ASC)
);

