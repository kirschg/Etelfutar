from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.common.keys import Keys
import time

try:
    #oldal megnyitása edge böngészővel
    driver=webdriver.Edge()
    driver.get("http://localhost:3000/")
    driver.maximize_window()
    #kezdőlap link megkeresése az oldalon amely átírányít a kezdőlapra
    #Bejelentkezés Teszt
    driver.find_element(By.LINK_TEXT,"login").click()
    #username,password mező megkeresése és automatikus kitöltése
    username_box=driver.find_element(By.ID,"usernameInput")
    username_box.clear()
    username_box.send_keys("TakacsL")
    time.sleep(1)
    password_box=driver.find_element(By.ID,"exampleInputPassword1")
    password_box.clear()
    password_box.send_keys("TakacsLaszlo")
    time.sleep(2)
    login_gomb=driver.find_element(By.XPATH, "//button[text()='Login']").click()
    time.sleep(2)
    driver.find_element(By.LINK_TEXT,"logout").click()
    #Regisztráció teszt
    time.sleep(2)
    driver.find_element(By.LINK_TEXT, "register").click()
    time.sleep(2)
    username_box=driver.find_element(By.ID,"userNameInput")
    username_box.clear()
    username_box.send_keys("test10")
    time.sleep(1)
    email_box=driver.find_element(By.ID,"emailInput")
    email_box.clear()
    email_box.send_keys("example@gmail.com")
    time.sleep(1)
    fullname_box=driver.find_element(By.ID,"fullNameInput")
    fullname_box.clear()
    fullname_box.send_keys("Kiss Pista")
    time.sleep(1)
    password_box=driver.find_element(By.ID,"passwordInput")
    password_box.clear()
    password_box.send_keys("TestPassword")
    passwordagain_box=driver.find_element(By.ID,"passwordAgainInput")
    passwordagain_box.clear()
    passwordagain_box.send_keys("TestPassword")
    time.sleep(1)
    streetnumber_box=driver.find_element(By.ID,"streetNumberInput")
    streetnumber_box.clear()
    streetnumber_box.send_keys("Kiss Pista utca 8")
    city_select=driver.find_element(By.ID,"cityInput")
    city_select.send_keys("Budapest")
    time.sleep(20)
    register_gomb=driver.find_element(By.XPATH, "//button[text()='Register']").click()
    time.sleep(10)

finally:
    driver.quit()