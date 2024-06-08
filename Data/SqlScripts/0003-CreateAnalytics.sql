DROP TABLE IF EXISTS analytics;

CREATE TABLE analytics (
    id SERIAL PRIMARY KEY,
    datakey TEXT, -- Can be null
    datavalue TEXT, -- Can be null
    referer TEXT, -- Can be null
    ipaddress TEXT,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT unique_referer_ipaddress_datakey UNIQUE (referer, ipaddress, datakey)
);
