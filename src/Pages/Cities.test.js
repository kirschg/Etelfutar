import React from 'react';
import { render, screen, waitFor } from '@testing-library/react';
import { Cities } from "./Cities";
import '@testing-library/jest-dom';
import { MemoryRouter } from 'react-router-dom';
const fetch = require('node-fetch');
global.fetch = fetch;


describe('Cities - fetch-testing', () => {
    it('endpoint datas expect tobe greater than 0',() => {
        render(
            <MemoryRouter>
                <Cities />
            </MemoryRouter>
        );
        waitFor(async() => {
            const response = await fetch('https://localhost:7106/Varosok/GetVarosokAsync');
            const cities = await response.json();
            expect(cities.length).toBeGreaterThan(0);
        });
    });
});