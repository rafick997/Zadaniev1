CREATE TABLE [dbo].[TreeNodes] (
    [NodeId]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (MAX) NULL,
    [ParentId]      INT            NULL,
    [Parent_NodeId] INT            NULL,
    CONSTRAINT [PK_dbo.TreeNodes] PRIMARY KEY CLUSTERED ([NodeId] ASC),
    CONSTRAINT [FK_dbo.TreeNodes_dbo.TreeNodes_Parent_NodeId] FOREIGN KEY ([Parent_NodeId]) REFERENCES [dbo].[TreeNodes] ([NodeId]));