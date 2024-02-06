#!/bin/bash

docker run --name coalim-postgres -e POSTGRES_PASSWORD=coalim -e POSTGRES_USER=coalim -p 5432:5432 postgres
