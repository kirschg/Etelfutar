import React from 'react';
import { render, screen, fireEvent, waitFor } from "@testing-library/react";
import "@testing-library/jest-dom";
import { Login } from "./Login";
import { MemoryRouter } from "react-router-dom";
import axios from "axios";
import userEvent from "@testing-library/user-event";

// Mockoljuk a useNavigate függvényt, hogy elkerüljük a valódi navigációt
jest.mock("react-router-dom", () => ({
  ...jest.requireActual("react-router-dom"),
  useNavigate: jest.fn(),
}));

// Mockoljuk az axios kéréseket
jest.mock("axios");

describe("Login komponens", () => {
  test("Helyesen renderelődik a login komponens", () => {
    render(
      <MemoryRouter>
        <Login />
      </MemoryRouter>
    );

    expect(screen.getByLabelText("Username")).toBeInTheDocument();
    expect(screen.getByLabelText("Password")).toBeInTheDocument();
    expect(screen.getByRole("button", { name: Login })).toBeInTheDocument();
  });

  describe("Űrlapmezők", () => {
    it("űrlapmezők kitöltése", async () => {
        axios.get.mockResolvedValueOnce({ // axios kiváltása, egy előre definiált válasszal:
            data: {
                username: 'Németh Bence',
                password: 'Ab12345678'
            }
        });
        render(<MemoryRouter>
          <Login />
        </MemoryRouter>);
        await waitFor(() => {
        const usernameInput = screen.getByPlaceholderText("username");
        const passwordInput = screen.getByPlaceholderText("password");
        
        fireEvent.change(usernameInput, { target: { value: "Németh Bence" } });
        expect(usernameInput.value).toBe("Németh Bence");
        fireEvent.change(passwordInput, { target: { value: "Ab12345678" } });
        expect(passwordInput.value).toBe("Ab12345678");
        });
      });
    });
});
