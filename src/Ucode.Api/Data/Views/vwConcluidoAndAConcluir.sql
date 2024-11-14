create or Alter View[vwConcluidoAndAConcluir] as
SELECT 
    ControleAluno.UserId,
    MONTH(ControleAluno.DataFim) AS Mes,
    YEAR(ControleAluno.DataFim) AS Ano,
    COUNT(CASE WHEN ControleAluno.Status = 1 THEN 1 END) AS AConcluido,
    COUNT(CASE WHEN ControleAluno.Status = 2 THEN 1 END) AS Concluido,
    COUNT(ControleAluno.Status) AS Total
FROM ControleAluno
WHERE 
    ControleAluno.DataFim >= DATEADD(MONTH, -11, CAST(GETDATE() AS DATE))
    AND ControleAluno.DataFim < DATEADD(MONTH, 1, CAST(GETDATE() AS DATE))
GROUP BY
    ControleAluno.UserId,
    MONTH(ControleAluno.DataFim),
    YEAR(ControleAluno.DataFim)