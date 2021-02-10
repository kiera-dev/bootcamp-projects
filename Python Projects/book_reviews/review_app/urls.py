from django.urls import path, include
from django.conf.urls import url
from . import views

urlpatterns = [
    path('', views.index),
    path('register/', views.register),
    path('login/', views.login),
    path('logout/', views.logout),
    path('books/', views.books),
    path('users/<int:user_id>/', views.users),
    path('add/', views.add),
    path('add_review/', views.add_review),
    path('reviews/<int:book_id>/', views.reviews),
    path('del_review/<int:review_id>/', views.del_review),

]
