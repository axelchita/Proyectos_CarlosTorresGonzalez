﻿CREATE TABLE [dbo].[R_TRANS_SLAVE] (
    [ID_TRANS_SLAVE]    BIGINT NOT NULL,
    [ID_TRANSFORMATION] INT    NULL,
    [ID_SLAVE]          INT    NULL,
    PRIMARY KEY CLUSTERED ([ID_TRANS_SLAVE] ASC)
);

