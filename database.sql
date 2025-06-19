
CREATE TABLE cadastro (
    texto VARCHAR(255) NOT NULL,
    numero INTEGER NOT NULL UNIQUE CHECK (numero > 0),
    PRIMARY KEY (numero)
);

CREATE TABLE log_operacoes (
    id SERIAL PRIMARY KEY,
    operacao VARCHAR(10) NOT NULL,
    data_hora TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    numero INTEGER
);

CREATE OR REPLACE FUNCTION registrar_log()
RETURNS TRIGGER AS $$
BEGIN
    IF (TG_OP = 'INSERT') THEN
        INSERT INTO log_operacoes (operacao, numero) VALUES ('INSERT', NEW.numero);
    ELSIF (TG_OP = 'UPDATE') THEN
        INSERT INTO log_operacoes (operacao, numero) VALUES ('UPDATE', NEW.numero);
    ELSIF (TG_OP = 'DELETE') THEN
        INSERT INTO log_operacoes (operacao, numero) VALUES ('DELETE', OLD.numero);
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_log
AFTER INSERT OR UPDATE OR DELETE ON cadastro
FOR EACH ROW EXECUTE FUNCTION registrar_log();
