import "bootstrap/dist/css/bootstrap.css";
import axios from "axios";
import '../Style.css';
import React from 'react';
import '@testing-library/jest-dom';
import {waitFor, render, screen, fireEvent} from '@testing-library/react';
import {Login} from './Login';



jest.mock('react-router-dom', () => ({
  ...jest.requireActual("react-router-dom"),
  useNavigate: () => jest.fn(),
}));
jest.mock('axios');

describe("Login", () => {
  it("login mezők renderelése", async () => {
    axios.get.mockResolvedValueOnce({
      data: {
        username: 'Test User',
        password: 'TestPassword',
      }
    });
    render(<Login/>);
    await waitFor(() => {
      expect(screen.getByLabelText("Username")).toBeInTheDocument();
      expect(screen.getByLabelText("Password")).toBeInTheDocument();
    });
  })
  it("űrlapmezők kitöltése", async () => {
    render(<Login/>);
    await waitFor(() => {
      const usernameInput = screen.getByLabelText("Username");
      const passwordInput = screen.getByLabelText("Password");
      fireEvent.change(usernameInput, {target: {value: "Kiss Pista"}})
      expect(usernameInput.value).toBe("Kiss Pista");
      fireEvent.change(passwordInput, {target: {value: "Password"}})
      expect(passwordInput.value).toBe("Password");
    })
  })
})

