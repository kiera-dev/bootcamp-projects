import time
from datetime import datetime
from .models import User, Manager, Book, Author, Review
from django.shortcuts import render, redirect, HttpResponse
from django.contrib import messages
from dateutil.relativedelta import relativedelta
import bcrypt


def index(request):
  return render(request, "index.html")


def logout(request):
  del request.session['userid']
  return redirect('/')


def register(request):
  errors = User.objects.validator(request.POST)
  if len(errors) > 0:
    for key, value in errors.items():
      messages.error(request, value)
    return redirect('/')
  else:
    email = request.POST.get('email')
    first_name = request.POST.get('first_name')
    last_name = request.POST.get('last_name')
    password = request.POST['password']
    password_confirm = request.POST['password_confirm']
    birthday = request.POST['birthday']
    username = request.POST['username']
    if len(password) < 8:
      return HttpResponse('Password must be longer than 8 characters')
    pw_hash = bcrypt.hashpw(password.encode(), bcrypt.gensalt()).decode()
    User.objects.create(email=request.POST['email'], password=pw_hash, first_name=first_name, last_name=last_name, birthday=birthday, username=username)
    person = User.objects.filter(email=request.POST['email'])
    if person:
      logged_user = person[0]
      request.session['userid'] = logged_user.id
      return redirect('/books')


def login(request):
  person = User.objects.filter(email=request.POST['email'])
  if person:
    logged_user = person[0]
    if bcrypt.checkpw(request.POST['password'].encode(), logged_user.password.encode()):
      request.session['userid'] = logged_user.id
      print(request.session['userid'])
      print(logged_user.id)
    return redirect('/books')
  else:
    return redirect("/")


def books(request):
  if request.session.get('userid'):
    user = User.objects.get(id=request.session.get('userid'))
    recent_reviews = Review.objects.all().order_by('-created_at')[:3]
    all_reviews = Review.objects.all().order_by('-created_at')
    all_books = Book.objects.all()
    context = {
      'user': user,
      'recent_reviews': recent_reviews,
      'all_books': all_books,
      'all_reviews': all_reviews,
    }
    return render(request, "books.html", context)
  else:
    return HttpResponse('You do not have permission to do that.')
    # returning http responses for users that need to login, will add better messaging later.

def add(request):
  if request.session.get('userid'):
    user = User.objects.get(id=request.session.get('userid'))
    authors = Author.objects.all()
    context = {
      'user': user,
      'authors': authors,
    }

    return render(request, "add.html", context)



def add_review(request):
  if request.session.get('userid'):
    user_id = User.objects.get(id=request.session.get('userid'))
    title = request.POST.get('title')
    author = request.POST.get('author')
    review = request.POST.get('review')
    rating = request.POST.get('rating')
    try:
      Book.objects.get(title=title)
      print('trying')
    except:
      Book.objects.create(title=title)
      print('creating book')
    try: 
      Author.objects.get(name=author)
      print('trying')
    except:
      Author.objects.create(name=author)
      print('creating author')
    book = Book.objects.get(title=title)
    person = Author.objects.get(name=author)
    Review.objects.create(msg_content=review, user=user_id, book=book, author=person, rating=rating)
    return redirect(f'/reviews/{book.id}')
  else:
    return HttpResponse('You do not have permission to do that.')


def users(request, user_id): 
  if request.session.get('userid'):
    count = 0
    user = User.objects.get(id=user_id)
    user_reviews  = user.reviews.all()
    reviews = Review.objects.all()
    for review in user.reviews.all():
      count +=1
    context = {
      'user': user,
      'reviews': reviews,
      'count': count,
      'user_reviews': user_reviews,
    }
    return render(request, 'users.html', context)
  else:
    return HttpResponse('You do not have permission to do that.')


def reviews(request, book_id):
  if request.session.get('userid'):
    user_id = request.session.get('userid')
    book = Book.objects.get(id=book_id)
    reviews = book.reviews.all()
    context = {
      'reviews': reviews,
      'book': book,
      'user_id': user_id
    }
    return render(request, 'reviews.html', context)
  else:
    return HttpResponse('You do not have permission to do that.')


def del_review(request, review_id):
  if request.session.get('userid'):
    user_id = request.session.get('userid')
    review_info = Review.objects.get(id=review_id)
    # time_difference = (datetime.now().timestamp() - message_info.created_at.timestamp()) / 60
    # print(time_difference)
    # if review_info.user.id == request.session.get('userid') and time_difference < 30:
    review_info.delete()
    return redirect(f'/users/{user_id}')

  else:
    return HttpResponse('You do not have permission to do that.')



