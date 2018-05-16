
class Array
  def first_duplicate
    find {|e| count(e) > 1} #find -> pierwszy znaleziony element spelniajacy ...
  end
end
