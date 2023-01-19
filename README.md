# Spearman's Coefficient

This is a client-server application for calculating Spearman's rank correlation coefficient.

The user enters values that are sent to the server. 
The server calculates the Spearman's coefficient and draws conclusions about the correlation. 
The data entered by the user and the resulting outcomes are entered into the database.
The server then sends the results back to the client and they are displayed in the application window.

The server performs calculations for each client in a separate thread.

Attached scripts for creating database tables.

---

## About Spearman's coefficient

The Spearman’s rank coefficient of correlation or Spearman correlation coefficient is a nonparametric measure of rank correlation (statistical dependence of ranking between two variables).
Named after Charles Spearman, it is often denoted by the Greek letter ‘ρ’ (rho) and is primarily used for data analysis.
It measures the strength and direction of the association between two ranked variables.

The formula for calculating the Spearman coefficient:
![image](https://user-images.githubusercontent.com/47988040/213582984-2c7b91ec-fa2c-4f86-8e7e-cdcc9530b0cc.png)

Here,

n= number of data points of the two variables

d<sub>i</sub>= difference in ranks of the “ith” element

The Spearman Coefficient,⍴, can take a value between +1 to -1 where,

A ⍴ value of +1 means a perfect association of rank
A ⍴ value of 0 means no association of ranks
A ⍴ value of -1 means a perfect negative association between ranks.
Closer the ⍴ value to 0, weaker is the association between the two ranks.
