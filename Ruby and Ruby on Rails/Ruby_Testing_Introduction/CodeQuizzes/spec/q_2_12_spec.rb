require 'q_2_12'

describe "#merge" do
  it "converts array of hashes to a hash" do
    arr = [
      { a: 1, b: 2 },
      { a: 3, b: 24, c: 4 }
    ]
    expected = {
      a: [1, 3],
      b: [2, 24],
      c: [4]
    }
    expect(arr.merge).to eq expected
  end
end
