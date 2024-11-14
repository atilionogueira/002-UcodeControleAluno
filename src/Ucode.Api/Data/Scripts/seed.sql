INSERT INTO Aluno (Nome, Contato, Email, Instagram, Cidade, Estado, UserId)
VALUES 
('João Silva', '11912345678', 'joao.silva@example.com', '@joaosilva', 'São Paulo', 'SP', 'teste@balta.io'),
('Maria Oliveira', '11987654321', 'maria.oliveira@example.com', '@mariaoliveira', 'São Paulo', 'SP', 'teste@balta.io'),
('Pedro Santos', '11923456789', 'pedro.santos@example.com', '@pedrosantos', 'São Paulo', 'SP', 'teste@balta.io'),
('Ana Costa', '11934567890', 'ana.costa@example.com', '@anacosta', 'São Paulo', 'SP', 'teste@balta.io'),
('Lucas Almeida', '11945678901', 'lucas.almeida@example.com', '@lucasalmeida', 'São Paulo', 'SP', 'teste@balta.io'),
('Beatriz Rocha', '11956789012', 'beatriz.rocha@example.com', '@beatrizrocha', 'São Paulo', 'SP', 'teste@balta.io'),
('Gabriel Lima', '11967890123', 'gabriel.lima@example.com', '@gabriellima', 'São Paulo', 'SP', 'teste@balta.io'),
('Larissa Martins', '11978901234', 'larissa.martins@example.com', '@larissamartins', 'São Paulo', 'SP', 'teste@balta.io'),
('Felipe Souza', '11989012345', 'felipe.souza@example.com', '@felipesouza', 'São Paulo', 'SP', 'teste@balta.io'),
('Juliana Fernandes', '11990123456', 'juliana.fernandes@example.com', '@julianafernandes', 'São Paulo', 'SP', 'teste@balta.io');

*******************************************************************************************
INSERT INTO Curso (Nome, Descricao, DuracaoHoras, Categoria, UserId)
VALUES 
('Introdução à Programação', 'Curso básico de programação para iniciantes.', 20, 'Tecnologia', 'teste@balta.io'),
('Desenvolvimento Web com HTML e CSS', 'Curso de desenvolvimento web focado em HTML e CSS.', 15, 'Tecnologia', 'teste@balta.io'),
('Banco de Dados com SQL', 'Aprenda os fundamentos de SQL e bancos de dados relacionais.', 25, 'Tecnologia', 'teste@balta.io'),
('JavaScript para Iniciantes', 'Introdução ao JavaScript e sua aplicação em páginas web.', 30, 'Tecnologia', 'teste@balta.io'),
('Desenvolvimento Mobile com Flutter', 'Criação de apps mobile com Flutter e Dart.', 40, 'Tecnologia', 'teste@balta.io'),
('Lógica de Programação', 'Curso de lógica de programação e resolução de problemas.', 10, 'Tecnologia', 'teste@balta.io'),
('Inteligência Artificial Básica', 'Noções básicas de inteligência artificial e machine learning.', 35, 'Tecnologia', 'teste@balta.io'),
('C# para Iniciantes', 'Curso introdutório sobre a linguagem de programação C#.', 25, 'Tecnologia', 'teste@balta.io'),
('Desenvolvimento com ASP.NET Core', 'Introdução ao desenvolvimento com ASP.NET Core.', 50, 'Tecnologia', 'teste@balta.io'),
('Git e Controle de Versão', 'Fundamentos do Git e controle de versão para desenvolvedores.', 12, 'Tecnologia', 'teste@balta.io');

*************************************************************************************************************************************

INSERT INTO Modulo (SubTopico, Secao, CursoId, UserId)
VALUES 
('Introdução ao C#', 'Seção 1: Conceitos Básicos', 3, 'teste@balta.io'),          -- CursoId 3
('Fundamentos de HTML', 'Seção 1: Estrutura HTML', 4, 'teste@balta.io'),          -- CursoId 4
('Configuração de Ambiente', 'Seção 1: Iniciando com .NET e Angular', 5, 'teste@balta.io'),  -- CursoId 5
('SEO e Redes Sociais', 'Seção 1: Introdução ao Marketing Digital', 6, 'teste@balta.io'),  -- CursoId 6
('Primeiros Passos com Flutter', 'Seção 1: Ambiente e Ferramentas', 7, 'teste@balta.io'),  -- CursoId 7
('Comandos SQL Básicos', 'Seção 1: Introdução ao SQL', 8, 'teste@balta.io'),       -- CursoId 8
('JavaScript Essencial', 'Seção 1: Introdução ao JavaScript', 9, 'teste@balta.io'), -- CursoId 9
('Lógica e Algoritmos', 'Seção 1: Noções de Lógica', 10, 'teste@balta.io'),         -- CursoId 10
('Fundamentos de Machine Learning', 'Seção 1: Conceitos de IA', 11, 'teste@balta.io'),  -- CursoId 11
('Princípios de UX/UI', 'Seção 1: Introdução ao Design de Interfaces', 12, 'teste@balta.io'); -- CursoId 12

**************************************************************************************************************************************

INSERT INTO ControleAluno (DataInicio, DataFim, Resumo, Status, CursoId, ModuloId, UserId)
VALUES 
(GETDATE(), '2024-12-31', 'Resumo fictício do aluno no curso de BackEnd.', 1, 3, 9, 'teste@balta.io'),  -- Módulo 9
(GETDATE(), '2024-12-31', 'Resumo fictício do aluno no curso de FrontEnd.', 1, 3, 10, 'teste@balta.io'),  -- Módulo 10
(GETDATE(), '2024-12-31', 'Resumo fictício do aluno no curso FullStack.', 1, 3, 11, 'teste@balta.io'),  -- Módulo 11
(GETDATE(), '2024-12-31', 'Resumo fictício do aluno no curso de Marketing.', 1, 3, 12, 'teste@balta.io'),  -- Módulo 12
(GETDATE(), '2024-12-31', 'Resumo fictício do aluno no curso Mobile.', 1, 3, 13, 'teste@balta.io'),  -- Módulo 13
(GETDATE(), '2024-12-31', 'Resumo fictício do aluno no curso de Backend.', 1, 3, 14, 'teste@balta.io'),  -- Módulo 14
(GETDATE(), '2024-12-31', 'Resumo fictício do aluno no curso FrontEnd.', 1, 3, 15, 'teste@balta.io'),  -- Módulo 15
(GETDATE(), '2024-12-31', 'Resumo fictício do aluno no curso de Marketing.', 1, 3, 16, 'teste@balta.io'),  -- Módulo 16
(GETDATE(), '2024-12-31', 'Resumo fictício do aluno no curso de Mobile.', 1, 3, 17, 'teste@balta.io'),  -- Módulo 17
(GETDATE(), '2024-12-31', 'Resumo fictício do aluno no curso de FullStack.', 1, 3, 18, 'teste@balta.io');  -- Módulo 18
