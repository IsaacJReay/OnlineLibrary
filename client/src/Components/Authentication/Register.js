import React, { useState, useCallback, useEffect } from 'react';
import Form, { Input } from '../Form';

function Register(){;

  const [username, setUsername] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [passwordConfrimation, setPasswordConfirmation] = useState('');
  const [invalidUsername, setInvalidUsername] = useState('');
  const [invalidEmail, setInvalidEmail] = useState('');
  const [invalidPassword, setInvalidPassword] = useState('');
  const [invalidPasswordConfirmation, setInvalidPasswordComfirmation] = useState('');
  const [usernameValid, setUsernameValid] = useState(false);
  const [emailValid, setEmailValid] = useState(false);
  const [passwordValid, setPasswordValid] = useState(false);
  const [passwordConfirmationValid, setPasswordConfirmationValid] = useState(false); 

  const handleUsernameChange = useCallback(function(event){
    setUsername(event.target.value);

    if(event.target.value === ''){
      setInvalidUsername('Username is required');
      setUsernameValid(false);
    }else{
      setInvalidUsername('');
      setUsernameValid(true);
    }
  }, []);

  const handleEmailChange = useCallback(function(event){
    setEmail(event.target.value);

    if(event.target.value === ''){
      setInvalidEmail('Email is required');
      setEmailValid(false);
      return;
    }

    const regExp = /^([a-zA-Z0-9\.\-\_]+)@([a-zA-Z0-9]+)\.([a-z]{2,5})$/g;

    if(!regExp.test(event.target.value)){
      setInvalidEmail('Incorrect email');
      setEmailValid(false);
    }else{
      setInvalidEmail('');
      setEmailValid(true);
    }
  }, []);

  const handlePasswordChange = useCallback(function(event){
    setPassword(event.target.value);

    if(event.target.value === ''){
      setInvalidPassword('Password is required');
      setPasswordValid(false);
      return;
    }

    const regExp = /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$/g;

    if(!regExp.test(event.target.value)){
      setInvalidPassword('Password needs to be 8 characters long, at least one small letter, one capital letter, one number and one symbol');
      setPasswordValid(false);
    }else{
      setInvalidPassword('');
      setPasswordValid(true);
    }


  }, []);

  const handlePasswordConfirmationChange = useCallback(function(event){
    setPasswordConfirmation(event.target.value);

    if(event.target.value === ''){
      setInvalidPasswordComfirmation('Password confirmation is required');
      setPasswordConfirmationValid(false);
      return;
    }

    if(password !== event.target.value){
      setInvalidPasswordComfirmation('Password does not match');
      setPasswordConfirmationValid(false);
    }else{
      setInvalidPasswordComfirmation('');
      setPasswordConfirmationValid(true);
    }
  }, [password]);

  const handleSubmit = useCallback(function(event){
    event.preventDefault();
  }, []);

  return (
    <div>
      <Form 
        submitTitle="Register" 
        onSubmit={ handleSubmit }
        submitOptions={{
          disable: true
        }}
      >
        <Input 
          label="Username" 
          value={ username } 
          onInputChange={ handleUsernameChange }
          invalidMessage={ invalidUsername }
          type="text"
        />
        <Input 
          label="Email" 
          value={ email } 
          onInputChange={ handleEmailChange }
          invalidMessage={ invalidEmail }
          type="email"
        />
        <Input 
          label="Password" 
          value={ password } 
          onInputChange={ handlePasswordChange }
          invalidMessage={ invalidPassword }
          type="password"
        />
        <Input 
          label="Password confirmation" 
          value={ passwordConfrimation } 
          onInputChange={ handlePasswordConfirmationChange }
          invalidMessage={ invalidPasswordConfirmation }
          type="email"
        />
      </Form>
    </div>
  );
}

export default Register;