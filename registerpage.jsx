// make sure to configure the proxy server in the package.json file. paste this in: "proxy": "https://localhost:7167",


import React from 'react';
import { useState } from "react";
import '../App.css';
import SquareWithImageAndForm from '../components/SquareWithImageAndForm'; // Import the component

function Register() {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [error, setError] = useState("");

    const handleRegister = async () => {
        // Validate input fields
        if (!email || !password || !confirmPassword) {
            setError("Please fill in all fields.");
            return;
        }
        if (password !== confirmPassword) {
            setError("Passwords do not match.");
            return;
        }

        try {
            const response = await fetch("/register", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({
                    email: email,
                    password: password,
                }),
            });
            

            if (!response.ok) {
                const errorData = await response.json();
                setError(errorData.message && "Registration failed.");
                return;
            }
// If registration is successful, add the "Instructor" role to the user
        const userResponse = await fetch("/add-instructor-role", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                userEmail: email, // Assuming email is used as the identifier for the user
            }),
        });

        if (!userResponse.ok) {
            const errorData = await userResponse.json();
            setError(errorData.message || "Failed to add Instructor role to user.");
            return;
        }

        // Registration and role assignment successful
        setError("");
        console.log("User registered successfully and assigned the Instructor role!");
        // Redirect to login page or perform any other action
    } catch (error) {
        console.error("Error occurred during registration:", error);
        setError("An error occurred during registration. Please try again later.");
    }
};

    return (
        <div className="container">
            <h2>Register</h2>
            <form onSubmit={(e) => { e.preventDefault(); handleRegister(); }}>
                <div className="form-group">
                    <label>Email:</label>
                    <input
                        type="email"
                        className="form-control"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                    />
                </div>
                <div className="form-group">
                    <label>Password:</label>
                    <input
                        type="password"
                        className="form-control"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                    />
                </div>
                <div className="form-group">
                    <label>Confirm Password:</label>
                    <input
                        type="password"
                        className="form-control"
                        value={confirmPassword}
                        onChange={(e) => setConfirmPassword(e.target.value)}
                    />
                </div>
                {error && <p className="text-danger">{error}</p>}
                <button type="submit" className="btn btn-primary">Register</button>
            </form>
        </div>
    );
}

export default Register;
