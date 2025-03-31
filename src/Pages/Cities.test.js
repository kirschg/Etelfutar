import '@testing-library/jest-dom';
import { MemoryRouter } from 'react-router-dom';
import fetch from 'node-fetch';
global.fetch = fetch;

describe('Cities - fetch-testing', () => {
    it('endpoint datas expect tobe greater than 0', async () => {
        render(
            <MemoryRouter>
                <Cities />
            </MemoryRouter>
        );
        await waitFor(async() => {
            const response = await fetch('https://localhost:7106/Varosok/GetVarosokAsync');
            const cities = await response.json();
            expect(cities.length).toBeGreaterThan(0);
        });
    })
})