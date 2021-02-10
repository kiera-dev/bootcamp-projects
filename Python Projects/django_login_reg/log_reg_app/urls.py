from django.urls import path, include
from django.conf.urls import url
from . import views

urlpatterns = [
    path('', views.index),
    path('register/', views.register),
    path('login/', views.login),
    path('logout/', views.logout),
    path('success/', views.success),
]
