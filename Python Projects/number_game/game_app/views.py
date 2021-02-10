import random
from django.shortcuts import render, HttpResponse, redirect

def index(request):
  number = random.randint(1,100)
  request.session['number'] = number
  return render(request, 'index.html')

def submit_guess(request):


  guess_submission = request.POST['guess']
  guess_submission = int(guess_submission)
  if guess_submission == request.session['number']:
    return redirect('/win')
  elif guess_submission < request.session['number']:
    return redirect('/low')
  elif guess_submission > request.session['number']:
    return redirect('/high')
  else:
    return render(request, 'index.html')

def play_again(request):
  del request.session['number']
  return redirect('/')


def win(request):
  return render(request, 'win.html')

def low(request):
  return render(request, 'low.html')

def high(request):
  return render(request, 'high.html')
