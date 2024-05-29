desc keyspaces;

CREATE KEYSPACE [IF NOT EXISTS] quick_bite WITH REPLICATION = { 'class' : 'SimpleStrategy', 'replication_factor' : 1 }; 
-- SimpleStrategy
-- NetworkTopologyStrategy

USE nome_keyspace
USE nome_tabela
DESC nome_tabela
   